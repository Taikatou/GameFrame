using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace GameFrame.Controllers.Click.MouseClick
{
    public delegate void MouseEvent(MouseState mouseState, MouseState previousMouseState);
    public class MouseControl : IUpdate
    {
        private MouseState _previousState;
        private MouseState _mouseState;

        public MouseEvent OnPressedEvent;
        public MouseEvent OnHeldDownEvent;
        public MouseEvent OnReleaseEvent;

        public bool JustPressed => _previousState.LeftButton == ButtonState.Released &&
                                   _mouseState.LeftButton != ButtonState.Released;

        public bool HeldDown => _previousState.LeftButton == ButtonState.Released &&
                                _mouseState.LeftButton == ButtonState.Released;

        public bool JustReleased => _previousState.LeftButton == ButtonState.Released &&
                                    _mouseState.LeftButton != ButtonState.Released;

        public MouseControl()
        {
            _mouseState = Mouse.GetState();
        }
        public void Update(GameTime gameTime)
        {
            _previousState = _mouseState;
            _mouseState = Mouse.GetState();
            if (JustPressed)
            {
                OnPressedEvent?.Invoke(_mouseState, _previousState);
            }
            else if (HeldDown)
            {
                OnHeldDownEvent?.Invoke(_mouseState, _previousState);
            }
            else if (JustReleased)
            {
                OnReleaseEvent?.Invoke(_mouseState, _previousState);
            }
        }
    }
}
