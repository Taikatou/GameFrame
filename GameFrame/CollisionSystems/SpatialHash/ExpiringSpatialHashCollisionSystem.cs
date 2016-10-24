using System.Collections.Generic;
using GameFrame.Common;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.CollisionSystems.SpatialHash
{
    public class ExpiringSpatialHashCollisionSystem<T> : ISpatialCollisionSystem<T>, IUpdate
    {
        private readonly SpatialHashCollisionSystem<T> _spatialHash;
        public readonly Dictionary<Point, ExpiringKey> MovingEntities;

        public ExpiringSpatialHashCollisionSystem()
        {
            _spatialHash = new SpatialHashCollisionSystem<T>();
            MovingEntities = new Dictionary<Point, ExpiringKey>();
        }

        public bool Moving(Point position)
        {
            var moving = MovingEntities.ContainsKey(position);
            return moving;
        }

        public float Progress(Point position)
        {
            var progress = 0.0f;
            if (Moving(position))
            {
                progress = MovingEntities[position].Progress;
            }
            return progress;
        }

        public void AddNode(Point position, T node)
        {
            _spatialHash.AddNode(position, node);
        }

        public bool MoveNode(Point startPosition, Point endPosition, float timer)
        {
            var moving = MovingEntities.ContainsKey(startPosition);
            var collision = CheckCollision(endPosition);
            var validMove = !moving && !collision;
            if (validMove)
            {
                var node = ValueAt(startPosition);
                AddNode(endPosition, node);
                MovingEntities[startPosition] = new ExpiringKey(timer);
                MovingEntities[endPosition] = new ExpiringKey(timer);
            }
            return validMove;
        }

        public void RemoveNode(Point point)
        {
            _spatialHash.RemoveNode(point);
        }

        public void Update(GameTime gameTime)
        {
            var keysToRemove = new List<Point>();
            foreach (var item in MovingEntities)
            {
                var expiringKey = item.Value;
                expiringKey.Update(gameTime);
                if (expiringKey.Complete)
                {
                    keysToRemove.Add(item.Key);
                }
            }
            foreach (var key in keysToRemove)
            {
                var position = key;
                RemoveNode(position);
                MovingEntities.Remove(key);
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
