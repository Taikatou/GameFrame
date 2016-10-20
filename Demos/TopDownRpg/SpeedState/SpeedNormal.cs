using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    class SpeedNormal<T> : IState<int>
    {
        public int Modifier => 200;
    }
}
