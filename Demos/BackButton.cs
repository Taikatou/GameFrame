using GameFrame;
using GameFrame.CollisionSystems.SpatialHash;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Entities;
using System.Diagnostics;

namespace Demos
{
    public class BackButton : IRenderable
    {
        
        private readonly Texture2D buttonTexture;
        private Point _tileSize;
        public readonly Entity Entity;
        private readonly ExpiringSpatialHashCollisionSystem<Entity> _spaitalHash;
        public Rectangle FrameRectangle;
        public Vector2 Offset { get; }

       public Vector2 ScreenPosition
        {
            get
            {
                return new Vector2(50, 30);
            }
        }

        public Vector2 Position
        {
            get { return Entity.Position; }
            set { Entity.Position = value; }
        }

        public BackButton(ContentManager content)
        {
            buttonTexture = content.Load<Texture2D>("TopDownRPG/Character");
            Offset = new Vector2(_tileSize.X / 2, _tileSize.Y / 2);
            FrameRectangle = new Rectangle(new Point(), _tileSize);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, ScreenPosition, Color.White);
        }

        public bool Hit(Point point)
        {
            Debug.WriteLine("Screen Position   " + ScreenPosition);
            if (point.X == 50)
            {
                return true;
            }
            return false;
        }
    }
}
