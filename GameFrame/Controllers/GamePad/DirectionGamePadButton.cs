using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.GamePad
{
    public class DirectionGamePadButton : AbstractGamePadButton
    {
        private readonly JoyStick _joyStickButton;
        private readonly GamePadButton _gamePadButton;
        public DirectionGamePadButton(Buttons button, PlayerIndex player=PlayerIndex.One, bool leftStick=true)
        {
            Button = button;
            Player = player;
            _gamePadButton = new GamePadButton(button, player);
            _joyStickButton = new JoyStick(leftStick);
        }
        public override void Update(GamePadState state)
        {
            PreviouslyActive = Active;
            _gamePadButton.Update(state);
            _joyStickButton.Update(state);
            Active = _gamePadButton.Active || (_joyStickButton.Button == Button);
        }
    }
}
