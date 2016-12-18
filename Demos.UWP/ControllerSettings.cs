using GameFrame.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Demos.UWP
{
    public class ControllerSettings : IControllerSettings
    {
        public bool GamePadEnabled => true;
        public bool KeyBoardMouseEnabled => true;
        public bool TouchScreenEnabled => true;
    }
}
