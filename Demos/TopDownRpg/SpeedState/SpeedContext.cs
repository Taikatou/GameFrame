using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedContext
    {
        private readonly float _baseSpeed;
        public IStateModifier<float> Terrain { get; set; }
        public float TerainSpeed => Terrain?.Modifier ?? 1.0f;
        public IStateModifier<float> SpeedState { get; set; }
        public float StateSpeed => SpeedState?.Modifier ?? 1.0f;
        public float Speed => _baseSpeed * StateSpeed * TerainSpeed;

        public SpeedContext(float baseSpeed)
        {
            _baseSpeed = baseSpeed;
        }
    }
}
