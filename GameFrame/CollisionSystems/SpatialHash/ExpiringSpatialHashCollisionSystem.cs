using System.Collections.Generic;
using GameFrame.Common;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.CollisionSystems.SpatialHash
{
    public class ExpiringSpatialHashCollisionSystem<T> : ISpatialCollisionSystem<T>, IUpdate
    {
        private readonly SpatialHashCollisionSystem<T> _spatialHash;
        private readonly List<ExpiringKey<Point>> _expiringPositions;

        public ExpiringSpatialHashCollisionSystem()
        {
            _spatialHash = new SpatialHashCollisionSystem<T>();
            _expiringPositions = new List<ExpiringKey<Point>>();
        }

        public void AddNode(Point position, T node)
        {
            _spatialHash.AddNode(position, node);
        }

        public void MoveNode(Point startPosition, Point endPosition, int timer)
        {
            var validMove = CheckCollision(startPosition) && !CheckCollision(endPosition);
            if (validMove)
            {
                var node = ValueAt(startPosition);
                AddNode(endPosition, node);
            }
        }

        public bool CheckCollision(int x, int y)
        {
            return _spatialHash.CheckCollision(x, y);
        }

        public void RemoveNode(Point point)
        {
            _spatialHash.RemoveNode(point);
        }

        public void Update(GameTime gameTime)
        {
            var keysToRemove = new List<ExpiringKey<Point>>();
            foreach (var key in _expiringPositions)
            {
                key.Update(gameTime);
                if (key.Complete)
                {
                    keysToRemove.Add(key);
                }
            }
            foreach (var key in keysToRemove)
            {
                var position = key.Value;
                RemoveNode(position);
                _expiringPositions.Remove(key);
            }
        }

        public bool CheckCollision(Point p)
        {
            var found = _spatialHash.CheckCollision(p);
            return found;
        }

        public T ValueAt(Point position)
        {
            return _spatialHash.ValueAt(position);
        }
    }
}
