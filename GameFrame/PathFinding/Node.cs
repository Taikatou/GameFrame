using GameFrame.PathFinding.Heuristics;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding
{
    public class Node
    {
        private Node _parentNode;
        private readonly IHeuristic _heuristic;
        public Point Location { get; }
        public double G { get; private set; }
        public double H { get; }
        public double F => G + H;

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

        public double GetTraversalCost(Node from)
        {
            var h = _heuristic.GetTraversalCost(Location, from.Location);
            return h + from.G;
        }

        public Node(Point point, Point endLocation, IHeuristic heuristic, int max)
        {
            Location = point;
            _heuristic = heuristic;
            H = _heuristic.GetTraversalCost(Location, endLocation);
            G = max;
        }
    }
}
