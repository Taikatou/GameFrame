using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.SpeedState
{
    public class SpeedWater : ISpeedState
    {
        public int Speed { get; set; }

        public SpeedWater()
        {
            Speed = 100;
        }

        public void Increment()
        {
            throw new NotImplementedException();
        }

        public void Decrement()
        {
            throw new NotImplementedException();
        }
    }
}
