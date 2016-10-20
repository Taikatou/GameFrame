using GameFrame.State;

namespace GameFrame.SpeedState
{
    public class SpeedWater<T> : IState<int>
    {
        public int Modifier => 300;
    }
}
