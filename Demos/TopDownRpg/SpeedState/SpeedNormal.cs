using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedNormal : IState<float>
    {
        public float Modifier => 2.0f;
    }
}
