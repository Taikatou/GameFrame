using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.GamePad
{
    public class DirectionGamePadButton : JoyStickGamePadButton
    {
        private readonly GamePadButton _gamePadButton;

        public DirectionGamePadButton(Buttons button, PlayerIndex player=PlayerIndex.One, bool leftStick=true) : base(button, player, leftStick)
        {
            _gamePadButton = new GamePadButton(button, player);
        }

        public override void Update(GamePadState state)
        {
            PreviouslyActive = Active;
            _gamePadButton.Update(state);
            JoyStick.Update(state);
            Active = _gamePadButton.Active || JoyStick.Contains(Button);
        }
    }
}
