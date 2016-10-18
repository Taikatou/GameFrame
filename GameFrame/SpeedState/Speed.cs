using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.SpeedState
{
    public class Speed
    {
        private ISpeedState _speedState;

        public Speed(ISpeedState currentSpeedState)
        {
            _speedState = currentSpeedState;
        }

        public void ToGrass()
        {
            _speedState = new SpeedGrass();
        }

        public void ToMud()
        {
            _speedState = new SpeedMud();
        }

        public void ToWater()
        {
            _speedState = new SpeedWater();
        }

        public void SetState(ISpeedState speedState)
        {
            _speedState = speedState;
        }
    }
}
