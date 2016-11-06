using System;
using System.Collections.Generic;
using GameFrame.Common;
using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.CollisionSystems.SpatialHash
{
    public class ExpiringSpatialHashCollisionSystem<T> : AbstractSpatialHashCollisionSystem<T>, IUpdate where T : BaseMovable
    {
        private readonly SpatialHashCollisionSystem<T> _spatialHash;
        public readonly Dictionary<Point, ExpiringKey> MovingEntities;
        public readonly HashSet<Point> ToRemove;

        public ExpiringSpatialHashCollisionSystem(IPossibleMovements possibleMovements) : base(possibleMovements)
        {
            _spatialHash = new SpatialHashCollisionSystem<T>(possibleMovements);
            MovingEntities = new Dictionary<Point, ExpiringKey>();
            ToRemove = new HashSet<Point>();
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

        public override void AddNode(Point position, T node)
        {
            _spatialHash.AddNode(position, node);
        }

        public bool MoveNode(Point startPosition, Point endPosition, EventHandler onCompleteEvent, float timer)
        {
            var moving = MovingEntities.ContainsKey(startPosition);
            var collision = CheckMovementCollision(startPosition, endPosition);
            var validMove = !moving && !collision;
            if (validMove)
            {
                var node = ValueAt(startPosition);
                AddNode(endPosition, node);
                foreach (var position in PossibleMovements.PositionsToCheck(startPosition, endPosition))
                {
                    if (position != endPosition)
                    {
                        MovingEntities[position] = new ExpiringKey(timer);
                        ToRemove.Add(position);
                    }
                }
                MovingEntities[startPosition] = new ExpiringKey(timer);
                ToRemove.Add(startPosition);
                MovingEntities[endPosition] = new ExpiringKey(timer);
                MovingEntities[endPosition].OnCompleteEvent += onCompleteEvent;
            }
            return validMove;
        }

        public override void RemoveNode(Point point)
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
                if (ToRemove.Contains(key))
                {
                    RemoveNode(position);
                    ToRemove.Remove(key);
                }
                MovingEntities[key].InvokeCompleteEvent();
                MovingEntities.Remove(key);
                var entity = ValueAt(position);
                entity?.InvokeOnMoveCompleteEvent();
            }
        }

        public override bool CheckCollision(Point startPosition)
        {
            var found = _spatialHash.CheckCollision(startPosition) || MovingEntities.ContainsKey(startPosition);
            return found;
        }

        public override T ValueAt(Point position)
        {
            return _spatialHash.ValueAt(position);
        }
    }
}
