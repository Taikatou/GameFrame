using GameFrame;
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

        public Vector2 Position
        {
            get { return Entity.Position; }
            set { Entity.Position = value; }
        }

        public EntityRenderer(ContentManager content, Entity entity, Point tileSize)
        {
            _entityTexture = content.Load<Texture2D>("TopDownRpg/Character");
            Entity = entity;
            Offset = tileSize.ToVector2() / 2;
            FrameRectangle = new Rectangle(new Point(), tileSize);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_entityTexture, Position, FrameRectangle, Color.White);
        }   
    }
}