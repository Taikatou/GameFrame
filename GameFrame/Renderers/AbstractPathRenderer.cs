using System.Collections.Generic;
using GameFrame.Movers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameFrame.Renderers
{
    public abstract class AbstractPathRenderer
    {
        public MoverManager Mover;
        public BaseMovable Moving;

        protected AbstractPathRenderer(MoverManager mover, BaseMovable moving)
        {
            Mover = mover;
            Moving = moving;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var points = Mover.PathPoints(Moving);
            if (points != null)
            {
                Draw(spriteBatch, points);
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch, List<Point> points);
    }
}
