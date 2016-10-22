using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedWater : IStateModifier<float>
    {
        public float Modifier => 1.2f;
    }
}
