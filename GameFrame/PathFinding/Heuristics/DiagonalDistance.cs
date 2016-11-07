using System;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.Heuristics
{
    public class DiagonalDistance : IHeuristic
    {
        public double GetTraversalCost(Point location, Point otherLocation)
        {
            var deltaX = Math.Abs(otherLocation.X - location.X);
            var deltaY = Math.Abs(otherLocation.Y - location.Y);
            return Math.Max(deltaX, deltaY);
        }
    }
}
