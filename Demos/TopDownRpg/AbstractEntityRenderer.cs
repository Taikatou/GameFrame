using Demos.TopDownRpg.Entities;
using GameFrame;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demos.TopDownRpg
{
    public abstract class AbstractEntityRenderer : IFocusAble, IRenderable
    {
        public readonly Texture2D EntityTexture;
        public Point TileSize;
        public readonly Entity Entity;
        private readonly ExpiringSpatialHashCollisionSystem<Entity> _spaitalHash;
        public Rectangle Area => new Rectangle(Position.ToPoint(), TileSize);
        public abstract Rectangle FrameRectangle { get; }
        public Vector2 Offset { get; }

        public Vector2 Position
        {
            get
            {
                var value = (Entity.Position.ToPoint() * TileSize).ToVector2();
                var startPoint = Entity.Position.ToPoint();
                if (_spaitalHash.Moving(startPoint))
                {
                    var movedBy = Entity.FacingDirection * _spaitalHash.Progress(startPoint);
                    var directionOffset = movedBy * TileSize.ToVector2();
                    value -= directionOffset;
                }
                return value;
            }
            set { Entity.Position = value; }
        }

        public AbstractEntityRenderer(ContentManager content, ExpiringSpatialHashCollisionSystem<Entity> spaitalHash, Entity entity, Point tileSize)
        {
            EntityTexture = content.Load<Texture2D>($"TopDownRpg/{entity.SpriteSheet}");
            Entity = entity;
            TileSize = tileSize;
            Offset = TileSize.ToVector2() / 2;
            _spaitalHash = spaitalHash;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EntityTexture, Position, FrameRectangle, Color.White);
        }
    }
}