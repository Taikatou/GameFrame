using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Renderers
{
    public class PathRenderer
    {
        private readonly Texture2D _texture;
        private readonly Texture2D _endTexture;
        public PathRenderer(Texture2D texture, Texture2D endTexture)
        {
            _texture = texture;
            _endTexture = endTexture;
        }

        public void Draw(SpriteBatch spriteBatch, List<Point> points, Vector2 pointSize)
        {
            if (points.Count > 0)
            {
                var lastIndex = points.Count - 1;
                for (var i = 0; i < lastIndex; i++)
                {
                    spriteBatch.Draw(_texture, points[i].ToVector2() * pointSize, null, Color.White);
                }
                spriteBatch.Draw(_endTexture, points[lastIndex].ToVector2() * pointSize, null, Color.White);
            }
        }
    }
}
