using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameFrame.CollisionTest
{
    public class BBObject : IPrototype
    {
        readonly Texture2D _texture;
        public Vector2 Position;
        public Vector2 Velocity;


        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y, 
                    _texture.Width, 
                    _texture.Height);
            }
        }

        public BBObject(Texture2D texture, Vector2 position)
        {
            this._texture = texture;
            this.Position = position;
        }

        public BBObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            this._texture = texture;
            this.Position = position;
            this.Velocity = velocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public IPrototype Clone()
        {
            Debug.WriteLine("Cloning BBObject");
            return (BBObject)MemberwiseClone();
        }
    }
}
