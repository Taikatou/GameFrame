using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.GamePad
{
    public class JoyStickGamePadButton : AbstractGamePadButton
    {
        public JoyStick JoyStick { get; set; }
        public JoyStickGamePadButton(Buttons button, PlayerIndex player = PlayerIndex.One, bool leftStick = true)
        {
            Button = button;
            Player = player;
            JoyStick = new JoyStick(leftStick);
        }
        public override void Update(GamePadState state)
        {
            PreviouslyActive = Active;
            JoyStick.Update(state);
            Active = JoyStick.Contains(Button);
        }
    }
}
