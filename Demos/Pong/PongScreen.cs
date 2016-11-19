using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.Pong
{
    public class PongScreen : DemoScreen
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ViewportAdapter _viewPort;
        public int BattleProbability { get; set; }

        public PongScreen(ViewportAdapter viewPort, SpriteBatch spriteBatch) : base(viewPort, spriteBatch)
        {
            _viewPort = viewPort;
            _spriteBatch = spriteBatch;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            var pongGame = new PongGame();
            GameModes.Push(pongGame);
        }

        public override void Draw(GameTime gameTime)
        {
            CurrentGameMode.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}
