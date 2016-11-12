using GameFrame.Common;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.KeyBoard
{
    public class KeyButton : IButtonAble
    {
        public bool Active { get; set; }
        public bool PreviouslyActive { get; set; }
        public readonly Keys Key;
        public KeyboardUpdater KeyboardUpdater;
        public KeyButton(Keys key, KeyboardUpdater keyboardUpdater)
        {
            Key = key;
            KeyboardUpdater = keyboardUpdater;
        }
        public virtual void Update()
        {
            PreviouslyActive = Active;
            Active = KeyboardUpdater.KeyBoardState.IsKeyDown(Key);
        }
    }
}
