using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.CollisionTest
{
    public class BBObject
    {
        Texture2D texture;
        public Vector2 Position;
        public Vector2 Velocity;


        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }

        public BBObject(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.Position = position;
        }

        public BBObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.texture = texture;
            this.Position = position;
            this.Velocity = velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
