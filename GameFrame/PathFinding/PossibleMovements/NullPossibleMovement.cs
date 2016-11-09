using System.Collections.Generic;
using System.Diagnostics;
using GameFrame.PathFinding.Heuristics;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.PossibleMovements
{
    class NullPossibleMovement : IPossibleMovements
    {
        public IHeuristic Heuristic { get; set; }

        public NullPossibleMovement()
        {
            //do nothing
            Debug.WriteLine("Null Pattern reached");
        }

        public NullPossibleMovement(IHeuristic heuristic)
        {
            //do nothing
            Debug.WriteLine("Null Pattern reached");
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
            return new[]
            {
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0)
            };
        }

        public IEnumerable<Point> PositionsToCheck(Point startPoint, Point endPoint)
        {
            return new[]
            {
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0),
                new Point(0, 0)
            };
        }
    }
}
