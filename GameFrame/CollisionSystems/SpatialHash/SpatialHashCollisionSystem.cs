using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionSystems.SpatialHash
{
    public class SpatialHashCollisionSystem<T> : ICollisionSystem
    {
        private readonly Dictionary<Point, T> _spatialHash;

        public SpatialHashCollisionSystem()
        {
            _spatialHash = new Dictionary<Point, T>();
        }

        public void AddNode(Point point, T node)
        {
            _spatialHash[point] = node;
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
    }
}
