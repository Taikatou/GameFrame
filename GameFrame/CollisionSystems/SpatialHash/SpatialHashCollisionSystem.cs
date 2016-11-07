using System.Collections.Generic;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionSystems.SpatialHash
{
    public class SpatialHashCollisionSystem<T> : AbstractSpatialHashCollisionSystem<T>
    {
        public readonly Dictionary<Point, T> SpatialHash;

        public SpatialHashCollisionSystem(IPossibleMovements possibleMovements) : base(possibleMovements)
        {
            SpatialHash = new Dictionary<Point, T>();
        }

        public override void AddNode(Point position, T node)
        {
            SpatialHash[position] = node;
        }

        public override void RemoveNode(Point position)
        {
            SpatialHash.Remove(position);
        }

        public override bool CheckCollision(Point startPoint)
        {
            var found = SpatialHash.ContainsKey(startPoint);
            return found;
        }

        public override T ValueAt(Point position)
        {
            var valueAt = default(T);
            if (CheckCollision(position))
            {
                valueAt = SpatialHash[position];
            }
            return valueAt;
        }
    }
}
