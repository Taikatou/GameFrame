namespace GameFrame
{
    public delegate void PushGameMode(IGameMode gameMode);
    public delegate IGameMode PopGameMode();
    public class GameModeController
    {
        public PushGameMode PushGameModeDelegate;
        public PopGameMode PopGameModeDelegate;

        public GameModeController(PushGameMode pushGameModeDelegate, PopGameMode popGameModeDelegate)
        {
            PushGameModeDelegate = pushGameModeDelegate;
            PopGameModeDelegate = popGameModeDelegate;
        }

        public GameModeController()
        {
            
        }

        public void PushGameMode(IGameMode mode)
        {
            PushGameModeDelegate.Invoke(mode);
        }

        public IGameMode PopGameMode()
        {
            return PopGameModeDelegate.Invoke();
        }
    }
}
