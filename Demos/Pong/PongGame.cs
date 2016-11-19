using GameFrame;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFrame.CollisionTest;
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
        public BbObject PlayerOne;
        public BbObject PlayerTwo;
        public BbObject Ball;
        private readonly Originator _originator;
        private readonly Caretaker _caretaker;
        private readonly BbCollision _playerCollision;
        private readonly BbCollisionSubject _collisionSubject;
        private readonly Texture2D _paddleTexture;
        private readonly Texture2D _ballTexture;
        public List<IUpdate> UpdateList;
        public List<IRenderable> RenderList;
        private readonly ContentManager _content;

        public PongGame()
        {
            UpdateList = new List<IUpdate>();
            RenderList = new List<IRenderable>();
            _caretaker = new Caretaker();
            _originator = new Originator();
            _content = ContentManagerFactory.RequestContentManager();

            _ballTexture = _content.Load<Texture2D>("Pong/ball");
            _paddleTexture = _content.Load<Texture2D>("Pong/paddle");


            Vector2 position;
            position = new Vector2(0, 150);
            PlayerOne = new BbObject(_paddleTexture, position);

            position = new Vector2((800 - _paddleTexture.Width), 150);
            PlayerTwo = PlayerOne.Clone() as BbObject;
            PlayerTwo.Position = position;


            position = new Vector2(PlayerOne.BoundingBox.Right + 1, (350 - _ballTexture.Height) / 2);
            Ball = new BbObject(_ballTexture, position, new Vector2(2.0f, 2.0f));

            _originator.SetObject(PlayerOne.Position);
            _caretaker.AddMemento(_originator.CreateMemento());
            _originator.SetObject(PlayerTwo.Position);
            _caretaker.AddMemento(_originator.CreateMemento());
            _originator.SetObject(Ball.Position);
            _caretaker.AddMemento(_originator.CreateMemento());

            var objList = new List<BbObject> {Ball, PlayerOne, PlayerTwo};


            _playerCollision = new BbCollision(objList, ScreenSize.Width, ScreenSize.Height);

            _collisionSubject = _playerCollision.GetBbCollisionSubject();
            _collisionSubject.SetCollisionType("");
            _playerCollision.CheckCollision();

        }


        private void SetInStartPostion()
        {
            _originator.GetStateFromMemento(_caretaker.GetMemento(2));
            Ball.Position = _originator.GetSavedPosition();
            _originator.GetStateFromMemento(_caretaker.GetMemento(0));
            PlayerOne.Position = _originator.GetSavedPosition();
            _originator.GetStateFromMemento(_caretaker.GetMemento(1));
            PlayerTwo.Position = _originator.GetSavedPosition();
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