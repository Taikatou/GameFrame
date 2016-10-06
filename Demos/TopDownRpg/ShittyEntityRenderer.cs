using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Demos.TopDownRpg
{
    public class ShittyEntityRenderer : IMovable
    {
        private readonly Texture2D _entityTexture;
        private readonly Point _tileSize;
        public ShittyEntityRenderer(ContentManager content, Point position, Point tileSize)
        {
            _entityTexture = content.Load<Texture2D>("TopDownRpg/Character");
            _position = position;
            _tileSize = tileSize;
        }

        private Point _position;

        public Rectangle Rect => new Rectangle(_position * _tileSize, _tileSize);

        public Vector2 Position
        {
            get { return (_position* _tileSize).ToVector2(); }
            set { _position = value.ToPoint(); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_entityTexture, Rect, new Rectangle(new Point(), new Point(16, 16)), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}