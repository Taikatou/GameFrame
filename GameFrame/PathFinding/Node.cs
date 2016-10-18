using GameFrame.PathFinding.Heuristics;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding
{
    public class Node
    {
        private Node _parentNode;

        private readonly IHeuristic _heuristic;
        public Point Location { get; }

        public int G { get; private set; }

        public int H { get; }

        public int F => G + H;

        public Node ParentNode
        {
            get { return _parentNode; }
            set
            {
                // When setting the parent, also calculate the traversal cost from the start node to here (the 'G' value)
                _parentNode = value;
                G = _parentNode.G + _heuristic.GetTraversalCost(Location, _parentNode.Location);
            }
        }

        public Node(Point point, Point endLocation, IHeuristic heuristic, int max)
        {
            Location = point;
            _heuristic = heuristic;
            H = _heuristic.GetTraversalCost(Location, endLocation);
            G = max;
        }

        public override string ToString()
        {
            return $"{Location.X}, {Location.Y}";
        }
    }

    public enum NodeState { Untested, Open, Closed }
}
