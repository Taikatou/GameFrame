using System.Collections.Generic;
using GameFrame.Movers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Renderers
{
    public class EndPointPathRenderer : AbstractPathRenderer
    {
        private readonly Point _pointSize;
        private readonly Texture2D _endTexture;
        public EndPointPathRenderer(MoverManager mover, AbstractMovable moving, Texture2D endTexture, Point pointSize) : base(mover, moving)
        {
            _pointSize = pointSize;
            _endTexture = endTexture;
        }

        public override void Draw(SpriteBatch spriteBatch, List<Point> points)
        {
            var lastIndex = points.Count - 1;
            spriteBatch.Draw(_endTexture, (points[lastIndex] * _pointSize).ToVector2(), null, Color.White);
        }
    }
}
