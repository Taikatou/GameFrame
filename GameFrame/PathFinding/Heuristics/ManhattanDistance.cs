using System;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.Heuristics
{
    public class ManhattanDistance : IHeuristic
    {
        public int GetTraversalCost(Point location, Point otherLocation)
        {
            var deltaX = otherLocation.X - location.X;
            var deltaY = otherLocation.Y - location.Y;
            return Math.Abs(deltaX) + Math.Abs(deltaY);
        }
    }
}
