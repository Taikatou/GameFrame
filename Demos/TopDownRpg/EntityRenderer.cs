using GameFrame.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demos.TopDownRpg
{
    public class EntityRenderer : IFocusAble
    {
        private readonly Texture2D _entityTexture;
        private readonly Point _tileSize;
        private Point _position;
        public bool Walking;
        public Point Offset { get; }
        public Point ScreenPosition => _position * _tileSize;

        public Vector2 Position
        {
            get { return _position.ToVector2(); }
            set { _position = value.ToPoint(); }
        }

        public EntityRenderer(ContentManager content, Point position, Point tileSize)
        {
            _entityTexture = content.Load<Texture2D>("TopDownRpg/Character");
            _position = position;
            _tileSize = tileSize;
            Offset = new Point(_tileSize.X/2, _tileSize.Y/2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var rect = new Rectangle(ScreenPosition, _tileSize);
            spriteBatch.Draw(_entityTexture, rect, new Rectangle(new Point(), _tileSize), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}