﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace GameFrame.CollisionTest
{
    public class BbObject : IPrototype
    {
        readonly Texture2D _texture;
        public Vector2 Position;
        public Vector2 Velocity;


        public Rectangle BoundingBox => new Rectangle(
            (int)Position.X,
            (int)Position.Y, 
            _texture.Width, 
            _texture.Height);

        public BbObject(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            Position = position;
        }

        public BbObject(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            _texture = texture;
            Position = position;
            Velocity = velocity;
        }

        public Vector2 GetPosition()
        {
            return Position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Color.White);
        }

        public IPrototype Clone()
        {
            Debug.WriteLine("Cloning BBObject");
            return (BbObject)MemberwiseClone();
        }
    }
}
