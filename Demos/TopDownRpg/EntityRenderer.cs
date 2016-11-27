using GameFrame;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demos.TopDownRpg
{
    public class EntityRenderer : IFocusAble, IRenderable
    {
        private readonly Texture2D _entityTexture;
        private Point _tileSize;
        public readonly Entity Entity;
        private readonly ExpiringSpatialHashCollisionSystem<Entity> _spaitalHash;
        public Rectangle Area => new Rectangle(Position.ToPoint(), _tileSize);
        public Rectangle FrameRectangle;
        public Vector2 Offset { get; }

        public Vector2 Position
        {
            get
            {
                var value = (Entity.Position.ToPoint() * _tileSize).ToVector2();
                var startPoint = Entity.Position.ToPoint();
                if (_spaitalHash.Moving(startPoint))
                {
                    var movedBy = Entity.FacingDirection * _spaitalHash.Progress(startPoint);
                    var directionOffset = movedBy * _tileSize.ToVector2();
                    value -= directionOffset;
                }
                return value;
            }
            set { Entity.Position = value; }
        }

        public EntityRenderer(ContentManager content, ExpiringSpatialHashCollisionSystem<Entity> spaitalHash, Entity entity, Point tileSize)
        {
            _entityTexture = content.Load<Texture2D>($"TopDownRpg/{entity.SpriteSheet}");
            Entity = entity;
            _tileSize = tileSize;
            Offset = _tileSize.ToVector2();
            _spaitalHash = spaitalHash;
            FrameRectangle = new Rectangle(new Point(), tileSize);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_entityTexture, Position, FrameRectangle, Color.White);
        }
    }
}