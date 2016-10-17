using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameFrame.Paths
{
    public abstract class AbstractPath
    {
        public List<Point> PathPoints;
        public abstract Point NextPosition { get; }
        public bool ToMove;

        public void Update(Point currentLocation)
        {
            if (ToMove && currentLocation == NextPosition)
            {
                PathPoints.RemoveAt(0);
            }
        }
    }
}
