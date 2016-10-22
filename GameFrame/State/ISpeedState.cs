namespace GameFrame.SpeedState
{
    public interface ISpeedState
    {
        int Speed { get; set; }

        void Increment();
        void Decrement();
    }
}
