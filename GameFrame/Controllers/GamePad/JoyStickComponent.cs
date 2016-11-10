using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.GamePad
{
    public abstract class JoyStickComponent
    {
        float ThumbstickTolerance { get; set; }
        bool _leftStick { get; set; }
        Buttons Button { get; set; }
        public abstract void Update(GamePadState state);
    }
}
