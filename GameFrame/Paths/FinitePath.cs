using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameFrame.Paths
{
    public class FinitePath : AbstractPath
    {
        public override Point NextPosition => PathPoints[0];
        public bool ToMove => PathPoints.Count > 0;

        public FinitePath(List<Point> pathPoints)
        {
            PathPoints = pathPoints;
        }
    }
}
