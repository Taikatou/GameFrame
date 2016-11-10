using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.GamePad
{

    public abstract class JoystickDecorator : JoyStickComponent
    {
        private readonly JoyStickComponent _decoratedJoyStick;

        protected JoystickDecorator(JoyStickComponent decoratedJoyStick)
        {
            this._decoratedJoyStick = decoratedJoyStick;
        }

        protected JoystickDecorator()
        {
            //Do nothing
        }

        public float ThumbstickTolerance { get; set; }
        public bool _leftStick { get; set; }
        public Buttons Button { get; set; }

        public override void Update(GamePadState state)
        {
            _decoratedJoyStick.Update(state);
        }
    }
}