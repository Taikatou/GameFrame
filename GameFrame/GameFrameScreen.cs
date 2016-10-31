using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace GameFrame
{
    public abstract class GameFrameScreen : Screen
    {
        public GameModeStack GameModeStack;
        public IGameMode CurrentGameMode => GameModeStack.CurrentGameMode;
        public Stack<IGameMode> GameModes => GameModeStack.GameModes;
        protected GameFrameScreen()
        {
            GameModeStack = new GameModeStack();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (IsVisible)
            {
                CurrentGameMode.Update(gameTime);
            }
        }
    }
}
