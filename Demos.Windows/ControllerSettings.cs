﻿using GameFrame.Controllers;

namespace Demos.Windows
{
    public class ControllerSettings : IControllerSettings
    {
        public bool GamePadEnabled => true;
        public bool KeyBoardMouseEnabled => true;
        public bool TouchScreenEnabled => false;
    }
}
