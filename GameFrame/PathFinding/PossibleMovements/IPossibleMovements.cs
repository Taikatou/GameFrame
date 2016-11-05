using System.Collections.Generic;
using GameFrame.PathFinding.Heuristics;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.PossibleMovements
{
    public interface IPossibleMovements
    {
        IEnumerable<Point> GetAdjacentLocations(Point fromLocation, Point movementCircle);
        IHeuristic Heuristic { get; }
        IEnumerable<Point> PositionsToCheck(Point startPoint, Point endPoint);
    }
}
