using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedGrass : IStateModifier<float>
    {
        public float Modifier => 1.5f;
    }
}
