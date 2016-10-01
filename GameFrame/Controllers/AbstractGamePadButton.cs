using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Controllers
{
    public abstract class AbstractGamePadButton : IButtonAble
    {
        public bool Active { get; set; }
        public Buttons Button { get; set; }
        public bool PreviouslyActive { get; set; }
        public PlayerIndex Player;
        public bool Connected;

        public abstract void Update(GamePadState state);

        public virtual void Update()
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
