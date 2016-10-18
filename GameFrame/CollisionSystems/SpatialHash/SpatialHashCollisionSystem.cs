using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionSystems.SpatialHash
{
    public class SpatialHashCollisionSystem<T> : ISpatialCollisionSystem<T>
    {
        private readonly Dictionary<int, T> _spatialHash;
        private readonly int _width;

        public SpatialHashCollisionSystem(int width)
        {
            _width = width;
            _spatialHash = new Dictionary<int, T>();
        }

        public int HashKey(int x, int y)
        {
            return y*_width + x;
        }

        public int PointToHash(Point point)
        {
            return HashKey(point.X, point.Y);
        }

        public void AddNode(Point position, T node)
        {
            var hashPosition = PointToHash(position);
            _spatialHash[hashPosition] = node;
        }

        public void RemoveNode(Point position)
        {
            var hashPosition = PointToHash(position);
            _spatialHash.Remove(hashPosition);
        }

        public bool CheckCollision(int x, int y)
        {
            var hashPosition = HashKey(x, y);
            var found = _spatialHash.ContainsKey(hashPosition);
            return found;
        }

        public bool CheckCollision(Point position)
        {
            return CheckCollision(position.X, position.Y);
        }

        public T ValueAt(Point position)
        {
            var valueAt = default(T);
            if (CheckCollision(position))
            {
                var hashPosition = PointToHash(position);
                valueAt = _spatialHash[hashPosition];
            }
            return valueAt;
        }
    }
}
