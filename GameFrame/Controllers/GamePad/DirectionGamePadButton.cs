using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.GamePad
{
    public class DirectionGamePadButton : AbstractGamePadButton
    {
        private readonly GamePadButton _gamePadButton;
        public JoyStick JoyStick { get; set; }

        public DirectionGamePadButton(Buttons button, PlayerIndex player=PlayerIndex.One, bool leftStick=true)
        {
            Button = button;
            Player = player;
            _gamePadButton = new GamePadButton(button, player);
            JoyStick = new JoyStick(leftStick);
        }

        public override void Update(GamePadState state)
        {
            PreviouslyActive = Active;
            _gamePadButton.Update(state);
            JoyStick.Update(state);
            Active = _gamePadButton.Active || (JoyStick.Button == Button);
        }
    }
}
