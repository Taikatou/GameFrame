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

        public SpeedContext(float baseSpeed)
        {
            _baseSpeed = baseSpeed;
        }

        public float Speed
        {
            get
            {
                var speed = StateSpeed;
                var terainSpeed = TerainSpeed;
                return _baseSpeed * speed * terainSpeed;
            }
        }
    }
}
