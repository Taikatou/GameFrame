using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameFrame.Paths.Reversing
{
    public class ReversingPath : AbstractPath
    {
        private readonly ReversingCounter _counter;
        public override Point NextPosition => PathPoints[_counter.CurrentIndex];

        public Boolean ToMove => true;

        public ReversingPath(List<Point> pathPoints, int startIndex=0)
        {
            _counter = new ReversingCounter(startIndex, pathPoints.Count);
            PathPoints = pathPoints;
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
