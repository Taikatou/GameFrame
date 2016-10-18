using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.SpeedState
{
    public class SpeedMud : ISpeedState
    {
        public int Speed { get; set; }

        public SpeedMud()
        {
            Speed = 400;
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
