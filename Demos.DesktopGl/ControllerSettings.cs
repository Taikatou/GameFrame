using GameFrame.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Demos.DesktopGl
{
    public class ControllerSettings : IControllerSettings
    {
        public bool GamePadEnabled
        {
            get
            {
                var state = GamePad.GetState(PlayerIndex.One);
                return state.IsConnected;
            }
        }
        public bool KeyBoardMouseEnabled => true;
        public bool TouchScreenEnabled => false;
    }
}
