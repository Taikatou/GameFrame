using GameFrame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demos
{
    public class BackButton : IRenderable
    {
        private readonly Texture2D _buttonTexture;
        public Rectangle FrameRectangle;

        public BackButton(ContentManager content)
        {
            _buttonTexture = content.Load<Texture2D>("DemoScreen/BackButton");
            FrameRectangle = new Rectangle(15, 15, 50, 50);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_buttonTexture, FrameRectangle, Color.White);
        }

        public bool Hit(Point point)
        {
            return FrameRectangle.Contains(point);
        }
    }
}
