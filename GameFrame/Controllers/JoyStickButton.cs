using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameFrame.Controllers
{
    public class JoyStickButton : IButtonAble
    {
        public float ThumbstickTolerance = 0.35f;
        private readonly bool _leftStick;
        public bool Connected;
        public Buttons Button { get; set; }
        public bool Active { get; set; }
        public readonly PlayerIndex Player;
        public bool PreviouslyActive { get; set; }

        public JoyStickButton(bool leftStick = true, PlayerIndex player=PlayerIndex.One)
        {
            _leftStick = leftStick;
            Player = player;
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

        public void Update()
        {
            var capabilities = GamePad.GetCapabilities(Player);
            Connected = capabilities.IsConnected;
            if (Connected)
            {
                var state = GamePad.GetState(Player);
                Update(state);
            }
        }
    }
}
