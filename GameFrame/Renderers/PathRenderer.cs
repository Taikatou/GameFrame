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
        public int MaxX;
        public int MaxY;
        public PathRenderer(MoverManager mover, BaseMovable moving, Texture2D texture, Texture2D endTexture, Point pointSize, int maxX, int maxY) : 
            base(mover, moving)
        {
            _texture = texture;
            _endTexture = endTexture;
            _pointSize = pointSize;
            MaxX = maxX;
            MaxY = maxY;
        }

        public override void Draw(SpriteBatch spriteBatch, List<Point> points)
        {
            var lastIndex = points.Count - 1;
            int counter;
            for (counter = 0; counter < lastIndex && Contains(points[counter]); counter++)
            {
                spriteBatch.Draw(_texture, (points[counter] * _pointSize).ToVector2(), null, Color.White);
            }
            spriteBatch.Draw(_endTexture, (points[counter] * _pointSize).ToVector2(), null, Color.White);
        }

        public bool Contains(Point p)
        {
            return p.X <= MaxX && p.Y <= MaxY && p.X >= 0 && p.Y >= 0;
        }
    }
}
