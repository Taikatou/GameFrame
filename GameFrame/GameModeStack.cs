using System.Collections.Generic;

namespace GameFrame
{
    public class GameModeStack
    {
        public Stack<IGameMode> GameModes;

        public IGameMode CurrentGameMode => GameModes.Peek();

        public GameModeStack()
        {
            GameModes = new Stack<IGameMode>();
        }

        public void Unload()
        {
            var gameMode = GameModes.Pop();
            gameMode.Dispose();
        }
    }
}
