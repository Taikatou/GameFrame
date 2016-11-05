using System.Diagnostics;
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
        public readonly Entity Entity;
        public Rectangle FrameRectangle;
        public Vector2 Offset { get; }
        private Vector2 _tileSize;
        private ExpiringSpatialHashCollisionSystem<Entity> _spaitalHash;

        public Vector2 Position
        {
            get
            {
                var value = Entity.Position;
                /*var startPoint = Entity.Position.ToPoint();
                if (_spaitalHash.Moving(startPoint))
                {
                    var movedBy = Entity.MovingDirection * _spaitalHash.Progress(startPoint);
                    value = Entity.Position - movedBy;
                }*/
                return value;
            }
            set { Entity.Position = value; }
        }

        public EntityRenderer(ContentManager content, ExpiringSpatialHashCollisionSystem<Entity> spaitalHash, Entity entity, Point tileSize)
        {
            _spaitalHash = spaitalHash;
            _tileSize = tileSize.ToVector2();
            _entityTexture = content.Load<Texture2D>("TopDownRpg/Character");
            Entity = entity;
            Offset = tileSize.ToVector2() / 2;
            FrameRectangle = new Rectangle(new Point(), tileSize);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Debug.WriteLine(Position);
            spriteBatch.Draw(_entityTexture, Position, FrameRectangle, Color.White);
        }   
    }
}