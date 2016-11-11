using GameFrame;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFrame.CollisionTest;
using MonoGame.Extended.ViewportAdapters;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using GameFrame.Content;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Demos.Common;
// ReSharper disable PossibleLossOfFraction

namespace Demos.Pong
{
    public class PongGame : IGameMode
    {
        private string _type;
        private KeyboardState _keyboardState;
        public BBObject PlayerOne;
        public BBObject PlayerTwo;
        public BBObject Ball;
        private Originator originator;
        private Caretaker caretaker;
        private readonly BBCollision _playerCollision;
        private readonly BBCollisionSubject _collisionSubject;
        private readonly Texture2D _paddleTexture;
        private readonly Texture2D _ballTexture;
        public List<IUpdate> UpdateList;
        public List<IRenderable> RenderList;
        private ContentManager _content;
        private List<BBObject> _objList;

        public PongGame(ViewportAdapter viewPort)
        {
            UpdateList = new List<IUpdate>();
            RenderList = new List<IRenderable>();
            caretaker = new Caretaker();
            originator = new Originator();
            _content = ContentManagerFactory.RequestContentManager();

            _ballTexture = _content.Load<Texture2D>("Pong/ball");
            _paddleTexture = _content.Load<Texture2D>("Pong/paddle");


            Vector2 position;
            position = new Vector2(0, 150);
            PlayerOne = new BBObject(_paddleTexture, position);

            position = new Vector2((800 - _paddleTexture.Width), 150);
            PlayerTwo = PlayerOne.Clone() as BBObject;
            PlayerTwo.Position = position;


            position = new Vector2(PlayerOne.BoundingBox.Right + 1, (350 - _ballTexture.Height) / 2);
            Ball = new BBObject(_ballTexture, position, new Vector2(2.0f, 2.0f));

            originator.SetObject(PlayerOne.Position);
            caretaker.AddMemento(originator.CreateMemento());
            originator.SetObject(PlayerTwo.Position);
            caretaker.AddMemento(originator.CreateMemento());
            originator.SetObject(Ball.Position);
            caretaker.AddMemento(originator.CreateMemento());

            _objList = new List<BBObject>();
            _objList.Add(Ball);
            _objList.Add(PlayerOne);
            _objList.Add(PlayerTwo);


            _playerCollision = new BBCollision(_objList, ScreenSize.Width, ScreenSize.Height);

            _collisionSubject = _playerCollision.GetBbCollisionSubject();
            _collisionSubject.SetCollisionType("");
            _playerCollision.CheckCollision();

        }


        private void SetInStartPostion()
        {
            Ball.Position = originator.GetSavedBBObject();
            originator.GetStateFromMemento(caretaker.GetMemento(0));
            PlayerOne.Position = originator.GetSavedBBObject();
            originator.GetStateFromMemento(caretaker.GetMemento(1));
            PlayerTwo.Position = originator.GetSavedBBObject();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_ballTexture, Ball.Position, Color.White);
            spriteBatch.Draw(_paddleTexture, PlayerOne.Position, Color.White);
            spriteBatch.Draw(_paddleTexture, PlayerTwo.Position, Color.White);
            foreach (var toRender in RenderList)
            {
                toRender.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            Ball.Position += Ball.Velocity;

            _keyboardState = Keyboard.GetState();

            if (_keyboardState.IsKeyDown(Keys.W) && PlayerOne.BoundingBox.Y > 11)
                PlayerOne.Position.Y -= 10f;

            if (_keyboardState.IsKeyDown(Keys.S) && PlayerOne.BoundingBox.Y < ScreenSize.Height - PlayerOne.BoundingBox.Height - 11)
                PlayerOne.Position.Y += 10f;

            if (_keyboardState.IsKeyDown(Keys.Up) && PlayerTwo.BoundingBox.Y > 11)
                PlayerTwo.Position.Y -= 10f;

            if (_keyboardState.IsKeyDown(Keys.Down) && PlayerTwo.BoundingBox.Y < ScreenSize.Height - PlayerTwo.BoundingBox.Height - 11)
                PlayerTwo.Position.Y += 10f;

            _playerCollision.CheckCollision();
            _type = _collisionSubject.GetCollisionType();


            if (_type.Contains("Right"))
            {
                Debug.WriteLine("Player 1 Scored");
                SetInStartPostion();
                _collisionSubject.SetCollisionType("");
            }
            else if (_type.Contains("Left"))
            {
                Debug.WriteLine("Player 2 Scored");
                SetInStartPostion();
                _collisionSubject.SetCollisionType("");
            }

            else if (_type.Contains("Top"))
            {
                Ball.Velocity.Y *= -1;
                _collisionSubject.SetCollisionType("");
            }

            else if (_type.Contains("Bottom"))
            {
                Ball.Velocity.Y *= -1;
                _collisionSubject.SetCollisionType("");
            }
            else if (_type.Contains("Object"))
            {
                Ball.Velocity.X *= -1;
                _collisionSubject.SetCollisionType("");
            }
        }

        public void Dispose()
        {
            _content.Unload();
            _content.Dispose();
        }
    }
}