using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionSystems.SpatialHash
{
    public abstract class AbstractSpatialHashCollisionSystem<T> : AbstractCollisionSystem
    {
        public abstract void AddNode(Point position, T node);
        public abstract void RemoveNode(Point point);
        public abstract T ValueAt(Point position);

        protected AbstractSpatialHashCollisionSystem(IPossibleMovements possibleMovements) : base(possibleMovements)
        {
        }
    }
}
