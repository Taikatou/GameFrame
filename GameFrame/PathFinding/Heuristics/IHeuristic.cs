using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.Heuristics
{
    public interface IHeuristic
    {
        double GetTraversalCost(Point location, Point otherLocation);
    }
}
