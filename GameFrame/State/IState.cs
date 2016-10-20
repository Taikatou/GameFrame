namespace GameFrame.State
{
    public interface IState <out T>
    {
        T Modifier { get; }
    }
}
