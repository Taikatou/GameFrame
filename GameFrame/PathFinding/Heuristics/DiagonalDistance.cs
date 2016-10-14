using System;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.Heuristics
{
    public class DiagonalDistance : IHeuristic
    {
        public Int32 GetTraversalCost(Point location, Point otherLocation)
        {
            var deltaX = otherLocation.X - location.X;
            var deltaY = otherLocation.Y - location.Y;
            return Math.Max(deltaX, deltaY);
        }
    }
}
