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
        public Rectangle FrameRectangle;
        public Vector2 Offset { get; }

        public Vector2 ScreenPosition
        {
            get
            {
                var value = Entity.Position * _tileSize.ToVector2();
                var position = Entity.Position.ToPoint();
                if (_spaitalHash.Moving(position))
                {
                    var movedBy = Entity.FacingDirection * _spaitalHash.Progress(position);
                    var directionOffset = movedBy * _tileSize.ToVector2();
                    value -= directionOffset;
                }
                return value;
            }
        }

        public Vector2 Position
        {
            get { return Entity.Position; }
            set { Entity.Position = value; }
        }

        public EntityRenderer(ContentManager content, ExpiringSpatialHashCollisionSystem<Entity> spaitalHash, Entity entity, Point tileSize)
        {
            _entityTexture = content.Load<Texture2D>("TopDownRpg/Character");
            Entity = entity;
            _tileSize = tileSize;
            Offset = new Vector2(_tileSize.X / 2, _tileSize.Y / 2);
            _spaitalHash = spaitalHash;
            FrameRectangle = new Rectangle(new Point(), _tileSize);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_entityTexture, ScreenPosition, FrameRectangle, Color.White);
        }
    }
}