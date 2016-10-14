using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameFrame.Paths
{
    public class CyclicalPath : IPath
    {
        private readonly CyclicalCounter _counter;
        private List<Point> _pathPoints;
        public Point NextPosition => _pathPoints[_counter.CurrentIndex];

        public Boolean ToMove => true;

        public CyclicalPath(List<Point> pathPoints, int startIndex=0)
        {
            _counter = new CyclicalCounter(startIndex, pathPoints.Count);
            _pathPoints = pathPoints;
        }
        public void Update(Point currentLocation)
        {
            if(NextPosition == currentLocation)
            {
                _counter.Increment();
            }
        }
    }
}
