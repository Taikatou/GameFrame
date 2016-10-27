using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace GameFrame
{
    public abstract class GameFrameScreen : Screen
    {
        public Stack<IGameMode> GameModeStack;
        protected GameFrameScreen()
        {
            GameModeStack = new Stack<IGameMode>();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsVisible)
            {
                GameModeStack.Peek().Update(gameTime);
            }
        }
    }
}
