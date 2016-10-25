using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionSystems
{
    public abstract class AbstractCollisionSystem
    {
        public IPossibleMovements PossibleMovements;

        public AbstractCollisionSystem(IPossibleMovements possibleMovements)
        {
            PossibleMovements = possibleMovements;
        }

        public bool CheckMovementCollision(Point startPoint, Point endPoint)
        {
            var found = false;
            var postiionsToCheck = PossibleMovements.PositionsToCheck(startPoint, endPoint);
            foreach(var position in postiionsToCheck)
            {
                found = found || CheckCollision(position);
            }
            return found;
        }

        public abstract bool CheckCollision(Point startPoint);
    }
}
