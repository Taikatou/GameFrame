using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedRunning : IStateModifier<float>
    {
        public float Modifier => 2.0f;
    }
}
