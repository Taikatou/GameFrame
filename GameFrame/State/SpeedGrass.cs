using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.SpeedState
{
    public class SpeedGrass : ISpeedState
    {
        public int Speed { get; set; }

        public SpeedGrass()
        {
            Speed = 200;
        }

        public void Increment()
        {
            Speed += 20;
        }

        public void Decrement()
        {
            Speed -= 20;
        }
    }
}
