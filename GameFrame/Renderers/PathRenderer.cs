using System.Collections.Generic;
using GameFrame.Movers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Renderers
{
    public class PathRenderer : AbstractPathRenderer
    {
        private readonly Texture2D _texture;
        private readonly Texture2D _endTexture;
        private readonly Point _pointSize;
        public PathRenderer(MoverManager mover, BaseMovable moving, Texture2D texture, Texture2D endTexture, Point pointSize) : 
            base(mover, moving)
        {
            _texture = texture;
            _endTexture = endTexture;
            _pointSize = pointSize;
        }

        public override void Draw(SpriteBatch spriteBatch, List<Point> points)
        {
            var lastIndex = points.Count - 1;
            for (var i = 0; i < lastIndex; i++)
            {
                spriteBatch.Draw(_texture, (points[i] * _pointSize).ToVector2(), null, Color.White);
            }
            spriteBatch.Draw(_endTexture, (points[lastIndex] * _pointSize).ToVector2(), null, Color.White);
        }
    }
}
