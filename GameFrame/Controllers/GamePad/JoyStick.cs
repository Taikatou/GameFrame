using System;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.GamePad
{
    public class JoyStick : JoyStickComponent
    {
        public float ThumbstickTolerance { get; set; }
        public bool _leftStick { get; set; }
        public Buttons Button { get; set; }

        public JoyStick(bool leftStick = true)
        {
            _leftStick = leftStick;
        }

        
        public override void Update(GamePadState state)
        {
            var direction = _leftStick ? state.ThumbSticks.Left : state.ThumbSticks.Right;

            var absX = Math.Abs(direction.X);
            var absY = Math.Abs(direction.Y);
            if (absX > absY)
            {
                if (absX > ThumbstickTolerance)
                {
                    Button = direction.X > 0 ? Buttons.DPadRight : Buttons.DPadLeft;
                }
            }
            else if (absY > ThumbstickTolerance)
            {
                Button = direction.Y > 0 ? Buttons.DPadUp : Buttons.DPadDown;
            }
            else
            {
                Button = 0;
            }
        }
    }
}
