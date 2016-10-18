using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameFrame.Paths
{
    public class FinitePath : AbstractPath
    {
        public override Point NextPosition => PathPoints[0];

        public override bool ToMove
        {
            get
            {
                return PathPoints.Count > 0;
            }
        }

        public FinitePath(List<Point> pathPoints)
        {
            PathPoints = pathPoints;
        }

        public override void Update(Point currentLocation)
        {
            if (ToMove && currentLocation == NextPosition)
            {
                PathPoints.RemoveAt(0);
            }
        }
    }
}
