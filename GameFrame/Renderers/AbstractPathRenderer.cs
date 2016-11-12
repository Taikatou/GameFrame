using System.Collections.Generic;
using GameFrame.Movers;
using GameFrame.Paths;
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
            var path = Mover.GetPath(Moving);
            if (path != null && path.ToMove)
            {
                Draw(spriteBatch, path.PathPoints);
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch, List<Point> path);
    }
}
