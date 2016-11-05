using System.Collections.Generic;
using GameFrame.PathFinding.Heuristics;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.PossibleMovements
{
    public class EightWayPossibleMovement : IPossibleMovements
    {
        public IHeuristic Heuristic { get; set; }

        public EightWayPossibleMovement(IHeuristic heuristic)
        {
            Heuristic = heuristic;
        }

        public IEnumerable<Point> GetAdjacentLocations(Point fromLocation, Point movementCircle)
        {
            var originalLocations = FourWayPossibleMovement.FourWayAdjacentLocations(fromLocation, movementCircle);
            var adjacentLocations = new List<Point>(originalLocations)
            {
                //diagonally
                new Point(fromLocation.X-movementCircle.X, fromLocation.Y-movementCircle.Y),
                new Point(fromLocation.X+movementCircle.X, fromLocation.Y-movementCircle.Y),
                new Point(fromLocation.X+movementCircle.X, fromLocation.Y+movementCircle.Y),
                new Point(fromLocation.X-movementCircle.X, fromLocation.Y+movementCircle.Y)
            };
            return adjacentLocations;
        }

        public IEnumerable<Point> PositionsToCheck(Point startPoint, Point endPoint)
        {
            List<Point> positionsToCheck = new List<Point> { endPoint };
            if (startPoint.X != endPoint.X && startPoint.Y != endPoint.Y)
            {
                positionsToCheck.Add(new Point(startPoint.X, endPoint.Y));
                positionsToCheck.Add(new Point(endPoint.X, startPoint.Y));
            }
            return positionsToCheck;
        }
    }
}
