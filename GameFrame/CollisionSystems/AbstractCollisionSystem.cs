using System.Linq;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionSystems
{
    public abstract class AbstractCollisionSystem
    {
        public IPossibleMovements PossibleMovements;

        protected AbstractCollisionSystem(IPossibleMovements possibleMovements)
        {
            PossibleMovements = possibleMovements;
        }

        public bool CheckMovementCollision(Point startPoint, Point endPoint)
        {
            var positionsToCheck = PossibleMovements.PositionsToCheck(startPoint, endPoint);
            return positionsToCheck.Any(CheckCollision);
        }

        public abstract bool CheckCollision(Point startPoint);
    }
}
