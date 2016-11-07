using System;
using GameFrame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demos
{
    public class BackButton : IRenderable
    {

        private readonly Texture2D _buttonTexture;

        public Rectangle Area { get; set; }

        public BackButton(ContentManager content)
        {
            _buttonTexture = content.Load<Texture2D>("DemoScreen/BackButton");
            Area = new Rectangle(15, 15, 45, 45);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_buttonTexture, Area, Color.White);
        }

        public bool Hit(Point point)
        {
            return Area.Contains(point);
        }
    }
}
