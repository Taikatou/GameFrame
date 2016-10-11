using System;
using GameFrame.CollisionSystems.SpatialHash;
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
        private readonly Entity _entity;
        private readonly ExpiringSpatialHashCollisionSystem<Entity> _spaitalHash;
        public Point Offset { get; }

        public Point ScreenPosition
        {
            get
            {
                var value = _entity.Position.ToPoint() * _tileSize;
                var position = _entity.Position.ToPoint();
                if (_spaitalHash.Moving(position))
                {
                    var movedBy = _entity.Direction * _spaitalHash.Progress(position);
                    var directionOffset = movedBy * _tileSize.ToVector2();
                    value -= directionOffset.ToPoint();
                }
                return value;
            }
        }

        public Vector2 Position
        {
            get { return _entity.Position; }
            set { _entity.Position = value; }
        }

        public EntityRenderer(ContentManager content, ExpiringSpatialHashCollisionSystem<Entity> spaitalHash, Entity entity, Point tileSize)
        {
            _entityTexture = content.Load<Texture2D>("TopDownRpg/Character");
            _entity = entity;
            _tileSize = tileSize;
            Offset = new Point(_tileSize.X/2, _tileSize.Y/2);
            _spaitalHash = spaitalHash;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var rect = new Rectangle(ScreenPosition, _tileSize);
            spriteBatch.Draw(_entityTexture, rect, new Rectangle(new Point(), _tileSize), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}