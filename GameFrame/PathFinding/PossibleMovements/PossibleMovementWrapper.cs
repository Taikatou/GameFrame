using System.Collections.Generic;
using GameFrame.PathFinding.Heuristics;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.PossibleMovements
{
    public class PossibleMovementWrapper : IPossibleMovements
    {
        public IPossibleMovements PossibleMovements { internal get; set; }
        public IHeuristic Heuristic => PossibleMovements.Heuristic;

        public PossibleMovementWrapper(IPossibleMovements possibleMovements)
        {
            PossibleMovements = possibleMovements;
        }

        public IEnumerable<Point> GetAdjacentLocations(Point fromLocation)
        {
            return PossibleMovements.GetAdjacentLocations(fromLocation);
        }

        public IEnumerable<Point> PositionsToCheck(Point startPoint, Point endPoint)
        {
            return PossibleMovements.PositionsToCheck(startPoint, endPoint);
        }
    }
}
