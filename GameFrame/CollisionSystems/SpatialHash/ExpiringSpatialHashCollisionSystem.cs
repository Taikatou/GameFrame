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
        public readonly Dictionary<Point, ExpiringKey> OccupiedTiles;
        public readonly Dictionary<ExpiringKey, BaseMovable> MovingEntities;

        public ExpiringSpatialHashCollisionSystem(IPossibleMovements possibleMovements) : base(possibleMovements)
        {
            _spatialHash = new SpatialHashCollisionSystem<T>(possibleMovements);
            OccupiedTiles = new Dictionary<Point, ExpiringKey>();
            MovingEntities = new Dictionary<ExpiringKey, BaseMovable>();
        }

        public bool Moving(Point position)
        {
            var moving = OccupiedTiles.ContainsKey(position);
            return moving;
        }

        public override void AddNode(Point position, T node)
        {
            _spatialHash.AddNode(position, node);
        }

        public bool MoveNode(Point startPosition, Point endPosition, EventHandler onCompleteEvent, float timer)
        {
            var moving = OccupiedTiles.ContainsKey(startPosition);
            var collision = CheckMovementCollision(startPosition, endPosition);
            var validMove = !moving && !collision;
            if (validMove)
            {
                var node = ValueAt(startPosition);
                RemoveNode(startPosition);
                AddNode(endPosition, node);
                var movingEntity = new ExpiringKey(timer) { OnCompleteEvent = onCompleteEvent };
                foreach (var position in PossibleMovements.PositionsToCheck(startPosition, endPosition))
                {
                    OccupiedTiles[position] = movingEntity;
                }
                OccupiedTiles[startPosition] = movingEntity;
                MovingEntities[movingEntity] = node;
            }
            return validMove;
        }

        public override void RemoveNode(Point point)
        {
            _spatialHash.RemoveNode(point);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var moving in MovingEntities)
            {
                moving.Key.Update(gameTime);
            }
            var keysToRemove = new Dictionary<Point, ExpiringKey>();
            foreach (var item in OccupiedTiles)
            {
                var expiringKey = item.Value;
                if (expiringKey.Complete)
                {
                    keysToRemove[item.Key] = item.Value;
                }
            }
            foreach (var item in keysToRemove)
            {
                OccupiedTiles.Remove(item.Key);
                item.Value.InvokeCompleteEvent();
                MovingEntities[item.Value].InvokeOnMoveCompleteEvent();
            }
        }

        public override bool CheckCollision(Point startPosition)
        {
            var found = _spatialHash.CheckCollision(startPosition) || OccupiedTiles.ContainsKey(startPosition);
            return found;
        }

        public override T ValueAt(Point position)
        {
            return _spatialHash.ValueAt(position);
        }
    }
}
