using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace GameFrame.Common
{
    public class KeyboardUpdater : IUpdate
    {
        public KeyboardState KeyBoardState;

        public KeyboardUpdater()
        {
            KeyBoardState = Keyboard.GetState();
        }

        public void Update()
        {
            KeyBoardState = Keyboard.GetState();
        }

        public void Update(GameTime gameTime)
        {
            Update();
        }
    }
}
