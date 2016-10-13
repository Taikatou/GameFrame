using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.Heuristics
{
    public interface IHeuristic
    {
        int GetTraversalCost(Point location, Point otherLocation);
    }
}
