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
        public void Update()
        {
            var keyBoardState = Keyboard.GetState();
            Update(keyBoardState);
        }

        public void Update(KeyboardState keyBoardState)
        {
            PreviouslyActive = Active;
            Active = keyBoardState.IsKeyDown(Key);
        }
    }
}
