using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionSystems.SpatialHash
{
    public class SpatialHashCollisionSystem<T> : ISpatialCollisionSystem<T>
    {
        public readonly Dictionary<Point, T> SpatialHash;

        public SpatialHashCollisionSystem()
        {
            SpatialHash = new Dictionary<Point, T>();
        }

        public void AddNode(Point position, T node)
        {
            SpatialHash[position] = node;
        }

        public void RemoveNode(Point position)
        {
            SpatialHash.Remove(position);
        }

        public bool CheckCollision(Point position)
        {
            var found = SpatialHash.ContainsKey(position);
            return found;
        }

        public T ValueAt(Point position)
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
