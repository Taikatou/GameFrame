using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameFrame.Controllers
{
    public class JoyStickButton
    {
        public Buttons CachedButton;
        public Buttons Button
        {
            get
            {
                var capabilities = GamePad.GetCapabilities(PlayerIndex.One);
                if (capabilities.IsConnected)
                {
                    var state = GamePad.GetState(PlayerIndex.One);
                    Update(state);
                }
                return CachedButton;
            }
        }
        public float ThumbstickTolerance = 0.35f;
        private readonly bool _leftStick;

        public JoyStickButton(bool leftStick = true)
        {
            _leftStick = leftStick;
        }

        public void Update(GamePadState state)
        {
            var direction = _leftStick ? state.ThumbSticks.Left : state.ThumbSticks.Right;

            var absX = Math.Abs(direction.X);
            var absY = Math.Abs(direction.Y);
            if (absX > absY)
            {
                if (absX > ThumbstickTolerance)
                {
                    CachedButton = direction.X > 0 ? Buttons.DPadRight : Buttons.DPadLeft;
                }
            }
            else if (absY > ThumbstickTolerance)
            {
                CachedButton = direction.Y > 0 ? Buttons.DPadUp : Buttons.DPadDown;
            }
            else
            {
                CachedButton = 0;
            }
        }
    }
}
