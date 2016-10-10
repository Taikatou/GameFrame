using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.GamePad
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
            var capabilities = Microsoft.Xna.Framework.Input.GamePad.GetCapabilities(Player);
            Connected = capabilities.IsConnected;
            if (Connected)
            {
                var state = Microsoft.Xna.Framework.Input.GamePad.GetState(Player);
                Update(state);
            }
        }
    }
}
