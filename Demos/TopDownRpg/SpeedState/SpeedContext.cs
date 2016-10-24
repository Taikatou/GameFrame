using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedContext
    {
        private IStateModifier<float> _speed;

        public SpeedContext(IStateModifier<float> speedState)
        {
            _speed = speedState;
        }

        public float GetSpeed(float baseSpeed)
        {
            return baseSpeed * _speed.Modifier;
        }

        public void SetSpeed(IStateModifier<float> state )
        {
            _speed = state;
        }
    }
}
