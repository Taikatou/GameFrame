using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedNormal : IStateModifier<float>
    {
        public float Modifier => 2.0f;
    }
}
