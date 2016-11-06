using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.CollisionTest
{
    public class BBObject
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;

        public Rectangle BoundingBox => new Rectangle((int)Position.X,(int)Position.Y,Texture.Width,Texture.Height);

        public BBObject(Texture2D texture, Vector2 position)
        {
            this.Texture = texture;
            Position = position;
        }

        public BBObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this.Texture = texture;
            Position = position;
            Velocity = velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
