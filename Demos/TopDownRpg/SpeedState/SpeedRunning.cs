using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedRunning : IState<float>
    {
        public float Modifier => 0.5f;
    }
}
