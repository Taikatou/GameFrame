using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.GamePad
{
    public class GamePadButton : AbstractGamePadButton
    {
        public GamePadButton(Buttons button, PlayerIndex player = PlayerIndex.One)
        {
            Button = button;
            Player = player;
        }

        public override void Update(GamePadState state)
        {
            PreviouslyActive = Active;
            Active = state.IsButtonDown(Button);
        }
    }
}
