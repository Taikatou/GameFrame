using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.GamePad
{
    class ConcreteJoyStickDecorator : JoystickDecorator
    {
        public override void Update(GamePadState state)
        {
            base.Update(state);
            AddedBehaviour();
        }

        private void AddedBehaviour()
        {
            Debug.WriteLine("Example decorator");
        }
    }
}
