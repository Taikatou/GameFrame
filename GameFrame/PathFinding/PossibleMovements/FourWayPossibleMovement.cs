using System.Collections.Generic;
using GameFrame.PathFinding.Heuristics;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.PossibleMovements
{
    public class FourWayPossibleMovement : IPossibleMovements
    {
        public IHeuristic Heuristic { get; set; }

        public FourWayPossibleMovement()
        {
            Heuristic = new ManhattanDistance();
        }

        public FourWayPossibleMovement(IHeuristic heuristic)
        {
            Heuristic = heuristic;
        }

        public static IEnumerable<Point> FourWayAdjacentLocations(Point fromLocation, Point movementCircle)
        {
            return new[]
            {
                new Point(fromLocation.X-movementCircle.X, fromLocation.Y  ),
                new Point(fromLocation.X,   fromLocation.Y+movementCircle.Y),
                new Point(fromLocation.X+movementCircle.X, fromLocation.Y  ),
                new Point(fromLocation.X,   fromLocation.Y-movementCircle.Y)
            };
        }

        public IEnumerable<Point> GetAdjacentLocations(Point fromLocation, Point movementCircle)
        {
            return FourWayAdjacentLocations(fromLocation, movementCircle);
        }

        public IEnumerable<Point> PositionsToCheck(Point startPoint, Point endPoint)
        {
            return new List<Point> { endPoint };
        }
    }
}
