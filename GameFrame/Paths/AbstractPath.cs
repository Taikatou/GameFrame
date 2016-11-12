using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameFrame.Paths
{
    public abstract class AbstractPath
    {
        public List<Point> PathPoints;
        public abstract Point NextPosition { get; }
        public virtual bool ToMove { get; set; }
        public abstract void Update(Point currentLocation);
    }
}
