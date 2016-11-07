using GameFrame.Common;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.Heuristics
{
    public class CrowDistance : IHeuristic
    {
        public double GetTraversalCost(Point location, Point otherLocation)
        {
            var distance = Distance.GetDistance(location.ToVector2(), otherLocation.ToVector2());
            return distance;
        }
    }
}
