using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionSystems.SpatialHash
{
    public class SpatialHashCollisionSystem<T> : ISpatialCollisionSystem<T>
    {
        private readonly Dictionary<Point, T> _spatialHash;

        public SpatialHashCollisionSystem()
        {
            _spatialHash = new Dictionary<Point, T>();
        }

        public void AddNode(Point position, T node)
        {
            _spatialHash[position] = node;
        }

        public void RemoveNode(Point point)
        {
            _spatialHash.Remove(point);
        }

        public bool CheckCollision(int x, int y)
        {
            var point = new Point(x, y);
            var found = CheckCollision(point);
            return found;
        }

        public bool CheckCollision(Point p)
        {
            var found = _spatialHash.ContainsKey(p);
            return found;
        }

        public T ValueAt(Point position)
        {
            var valueAt = default(T);
            if (CheckCollision(position))
            {
                valueAt = _spatialHash[position];
            }
            return valueAt;
        }
    }
}
