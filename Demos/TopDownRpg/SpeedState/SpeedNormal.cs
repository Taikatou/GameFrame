using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedNormal : IStateModifier<float>
    {
        public float Modifier => 1.0f;
    }
}
