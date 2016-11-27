using GameFrame.Common;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.KeyBoard
{
    public class KeyButton : IButtonAble
    {
        public bool Active { get; set; }
        public bool PreviouslyActive { get; set; }
        public readonly Keys Key;
        public KeyButton(Keys key)
        {
            Key = key;
        }
        public virtual void Update()
        {
            PreviouslyActive = Active;
            var keyboardState = Keyboard.GetState();
            Active = keyboardState.IsKeyDown(Key);
        }
    }
}
