namespace GameFrame.Controllers
{
    public interface IControllerSettings
    {
        bool GamePadEnabled { get; }
        bool KeyBoardMouseEnabled { get; }
        bool TouchScreenEnabled { get; }
    }
}
