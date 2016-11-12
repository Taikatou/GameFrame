using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.NullButton
{
    public abstract class NullButton : IButtonAble
    {
        public bool Active { get; }
        public Buttons Button { get; }
        public bool PreviouslyActive { get; }

        public abstract void Update(GamePadState state);

        public virtual void Update()
        {
            //do nothing
            Debug.WriteLine("Null button reached");
        }
    }
}
