using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedRunning<T> : IState<int>
    {
        public int Modifier => 100;
    }
}
