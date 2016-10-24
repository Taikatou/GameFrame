namespace GameFrame.State
{
    public interface IStateModifier <out T>
    {
        T Modifier { get; }
    }
}
