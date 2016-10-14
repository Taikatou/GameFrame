using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameFrame.Paths
{
    public class FinitePath : IPath
    {
        private List<Point> _pathPoints;
        public Point NextPosition => _pathPoints[0];
        public Boolean ToMove => _pathPoints.Count > 0;

        public FinitePath(List<Point> pathPoints)
        {
            _pathPoints = pathPoints;
        }
        public void Update(Point currentLocation)
        {
            if(ToMove && currentLocation == NextPosition)
            {
                _pathPoints.RemoveAt(0);
            }
        }
    }
}
