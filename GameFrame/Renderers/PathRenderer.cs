using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Renderers
{
    public class PathRenderer
    {
        private readonly Texture2D _texture;
        public PathRenderer(Texture2D texture)
        {
            _texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, List<Point> points, Vector2 tileSize)
        {
            foreach (var point in points)
            {
                spriteBatch.Draw(_texture, point.ToVector2() * tileSize, null, Color.White);
            }
        }
    }
}
