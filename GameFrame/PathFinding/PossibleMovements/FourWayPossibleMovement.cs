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

        public static IEnumerable<Point> FourWayAdjacentLocations(Point fromLocation)
        {
            return new[]
            {
                new Point(fromLocation.X-1, fromLocation.Y  ),
                new Point(fromLocation.X,   fromLocation.Y+1),
                new Point(fromLocation.X+1, fromLocation.Y  ),
                new Point(fromLocation.X,   fromLocation.Y-1)
            };
        }

        public IEnumerable<Point> GetAdjacentLocations(Point fromLocation)
        {
            return FourWayAdjacentLocations(fromLocation);
        }

        public IEnumerable<Point> PositionsToCheck(Point startPoint, Point endPoint)
        {
            return new List<Point> { endPoint };
        }
    }
}
