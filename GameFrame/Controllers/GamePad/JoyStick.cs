using System;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.GamePad
{
    public class JoyStick
    {
        public float ThumbstickTolerance = 0.35f;
        private readonly bool _leftStick;
        public Buttons HorizontalButton;
        public Buttons VerticalButton;

        public JoyStick(bool leftStick = true)
        {
            _leftStick = leftStick;
        }

        public bool Contains(Buttons button)
        {
            return HorizontalButton == button || VerticalButton == button;
        }

        public void Update(GamePadState state)
        {
            var direction = _leftStick ? state.ThumbSticks.Left : state.ThumbSticks.Right;
            var absX = Math.Abs(direction.X);
            var absY = Math.Abs(direction.Y);
            if (absX > ThumbstickTolerance)
            {
                HorizontalButton = direction.X > 0 ? Buttons.DPadRight : Buttons.DPadLeft;
            }
            else
            {
                HorizontalButton = 0;
            }
            if (absY > ThumbstickTolerance)
            {
                VerticalButton = direction.Y > 0 ? Buttons.DPadUp : Buttons.DPadDown;
            }
            else
            {
                VerticalButton = 0;
            }
        }
    }
}
