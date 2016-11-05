using System;
using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.CollisionTest;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using Microsoft.Xna.Framework.Input;
using GameFrame.Content;
using Microsoft.Xna.Framework.Content;

namespace Demos.Pong
{
    public class PongScreen : DemoScreen
    {
        GraphicsDeviceManager graphics;
        private readonly ContentManager _content;
        private readonly SpriteBatch _spriteBatch;
        private readonly ViewportAdapter _viewPort;
        public int BattleProbability { get; set; }

        public PongScreen(ViewportAdapter viewPort, SpriteBatch spriteBatch) : base(viewPort, spriteBatch)
        {
            _viewPort = viewPort;
            _spriteBatch = spriteBatch;
        }



        /* protected override void Initialize()
         {

             base.Initialize();
         }*/


        public override void LoadContent()
        {
            base.LoadContent();
            var pongGame = new PongGame(_viewPort, _content);
            GameModes.Push(pongGame);
        }
        

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            _spriteBatch.End();
            CurrentGameMode.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
