using GameFrame.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.GUI
{
    public class ImageButton : IButtonAble, IRenderable
    {

        private readonly Texture2D _buttonTexture;
        public Rectangle Area { get; set; }
        public bool Active => _mouseState.LeftButton == ButtonState.Pressed;
        public bool PreviouslyActive => _previousState.LeftButton == ButtonState.Pressed;

        private MouseState _previousState;
        private MouseState _mouseState;

        public ImageButton(Texture2D image, Rectangle size)
        {
            _buttonTexture = image;
            Area = size;
            _mouseState = Mouse.GetState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_buttonTexture, Area, Color.White);
        }

        public bool Hit(Point point)
        {
            return Area.Contains(point);
        }

        public void Update()
        {
            _previousState = _mouseState;
            _mouseState = Mouse.GetState();
        }
    }
}
