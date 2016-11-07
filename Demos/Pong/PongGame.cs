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

namespace Demos.Pong
{
    public class PongGame : IGameMode
    {
        private string _type;
        private KeyboardState _keyboardState;
        public BBObject PlayerOne;
        public BBObject PlayerTwo;
        public BBObject Ball;
        private readonly BBCollision _playerCollision;
        private readonly BBCollisionSubject _collisionSubject;
        private readonly Texture2D _paddleTexture;
        private readonly Texture2D _ballTexture;
        public List<IUpdate> UpdateList;
        public List<IRenderable> RenderList;
        private ContentManager _content;

        public PongGame(ViewportAdapter viewPort)
        {
            UpdateList = new List<IUpdate>();
            RenderList = new List<IRenderable>();
            _content = ContentManagerFactory.RequestContentManager();

            _ballTexture = _content.Load<Texture2D>("Pong/ball");
            _paddleTexture = _content.Load<Texture2D>("Pong/paddle");


            Vector2 position;
            position = new Vector2(0, (350));
            PlayerOne = new BBObject(_paddleTexture, position);

            position = new Vector2((800 - _paddleTexture.Width),(350 - _paddleTexture.Height) / 2);
            PlayerTwo = PlayerOne.Clone() as BBObject;
            PlayerTwo.Position = position;

           
            position = new Vector2(PlayerOne.BoundingBox.Right + 1,(350 - _ballTexture.Height) / 2);
            Ball = new BBObject(_ballTexture, position, new Vector2(2.0f, 2.0f));

           
            _playerCollision = new BBCollision(Ball, PlayerOne, PlayerTwo, ScreenSize.Width, ScreenSize.Height);
        
            _collisionSubject = _playerCollision.GetBbCollisionSubject();

            _collisionSubject.SetVelocity(Ball.Velocity);
            _playerCollision.SetVelocity(Ball.Velocity);
            _playerCollision.CheckTwoPlayerCollision();
            _collisionSubject.SetCollisionType("");
        }
 

        private void SetInStartPostion()
        {
            PlayerOne.Position.Y = (350 - PlayerOne.BoundingBox.Height) / 2;
            PlayerTwo.Position.Y = (350 - PlayerTwo.BoundingBox.Height) / 2;
            Ball.Position.X = PlayerOne.BoundingBox.Right + 1;
            Ball.Position.Y = (350 - Ball.BoundingBox.Height) / 2;
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

            if (_keyboardState.IsKeyDown(Keys.W))
                PlayerOne.Position.Y -= 10f;

            if (_keyboardState.IsKeyDown(Keys.S))
                PlayerOne.Position.Y += 10f;

            if (_keyboardState.IsKeyDown(Keys.Up))
                PlayerTwo.Position.Y -= 10f;

            if (_keyboardState.IsKeyDown(Keys.Down))
                PlayerTwo.Position.Y += 10f;

            _playerCollision.CheckTwoPlayerCollision();
            Ball.Velocity = _collisionSubject.GetVelocity();
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

        }

        public void Dispose()
        {
            _content.Unload();
            _content.Dispose();
        }
    }
}
