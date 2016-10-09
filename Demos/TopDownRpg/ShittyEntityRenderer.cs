using GameFrame.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Demos.TopDownRpg
{
    public class ShittyEntityRenderer : IFocusAble
    {
        private readonly Texture2D _entityTexture;
        private Point _tileSize;
        private Point _position;
        public Vector2 ScreenPosition => Position * _tileSize.ToVector2();

        public ShittyEntityRenderer(ContentManager content, Point position, Point tileSize)
        {
            _entityTexture = content.Load<Texture2D>("TopDownRpg/Character");
            _position = position;
            _tileSize = tileSize;
        }

        public Vector2 Position
        {
            get { return _position.ToVector2(); }
            set { _position = value.ToPoint(); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var rect = new Rectangle(ScreenPosition.ToPoint(), _tileSize);
            spriteBatch.Draw(_entityTexture, rect, new Rectangle(new Point(), new Point(16, 16)), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}