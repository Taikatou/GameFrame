using GameFrame.State;

namespace Demos.TopDownRpg.SpeedState
{
    public class SpeedContext
    {
        private IState<float> _speed;

        public SpeedContext(IState<float> speedState)
        {
            _speed = speedState;
        }

        public float GetSpeed(float baseSpeed)
        {
            return baseSpeed * _speed.Modifier;
        }

        public void SetSpeed(IState<float> state )
        {
            _speed = state;
        }
    }
}
