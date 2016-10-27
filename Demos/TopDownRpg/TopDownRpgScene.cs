using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.PathFinding.Heuristics;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.TopDownRpg
{
    public class TopDownRpgScene : DemoScreen
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ViewportAdapter _viewPort;
        public TopDownRpgScene(ViewportAdapter viewPort, SpriteBatch spriteBatch) : base(viewPort, spriteBatch)
        {
            _viewPort = viewPort;
            _spriteBatch = spriteBatch;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            GameModeStack.Push(new OpenWorldGameMode(_viewPort, new EightWayPossibleMovement(new CrowDistance())));
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsVisible)
            {
                GameModeStack.Peek().Draw(_spriteBatch);
            }
        }
    }
}
