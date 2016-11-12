using GameFrame.Common;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Controllers.KeyBoard
{
    public class KeyButton : IButtonAble
    {
        public bool Active { get; set; }
        public bool PreviouslyActive { get; set; }

        public readonly Keys Key;
        private readonly KeyboardUpdater _keyboardUpdater;

        public KeyButton(Keys key, KeyboardUpdater keyboardUpdater)
        {
            Key = key;
            _keyboardUpdater = keyboardUpdater;
        }
        public void Update()
        {
            PreviouslyActive = Active;
            Active = _keyboardUpdater.KeyBoardState.IsKeyDown(Key);
        }
    }
}
