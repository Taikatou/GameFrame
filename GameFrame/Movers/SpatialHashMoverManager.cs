using System.Collections.Generic;
using GameFrame.CollisionSystems;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.Common;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public class SpatialHashMoverManager<T> : IMoverManager<T>, IUpdate where T : BaseMovable
    {
        private readonly AbstractCollisionSystem _abstractCollisionSystem;
        private readonly ExpiringSpatialHashCollisionSystem<T> _spatialHashLayer;
        private readonly List<T> _characterList;

        public SpatialHashMoverManager(AbstractCollisionSystem abstractCollisionSystem, ExpiringSpatialHashCollisionSystem<T> spatialHashLayer)
        {
            _abstractCollisionSystem = abstractCollisionSystem;
            _characterList = new List<T>();
            _spatialHashLayer = spatialHashLayer;
        }

        public void Add(T character)
        {
            _characterList.Add(character);
            _spatialHashLayer.AddNode(character.Position.ToPoint(), character);
        }

        public bool RequestMovement(T character, Vector2 position)
        {
            var startPoint = character.Position.ToPoint();
            var endPoint = position.ToPoint();
            if (!_abstractCollisionSystem.CheckMovementCollision(startPoint, endPoint))
            {
                var distance = (float)Distance.GetDistance(character.Position, position);
                var timer = distance/character.Speed;
                timer *= 1000; //milliseconds in second
                var moveEvent = character.OnMoveEvent + character.OnMoveCompleteEvent;
                if (_spatialHashLayer.MoveNode(startPoint, endPoint, moveEvent, timer))
                {
                    character.Position = position;
                    return true;
                }
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var character in _characterList)
            {
                if (character.Moving)
                {
                    var position = character.Position + character.MovingDirection;
                    if (RequestMovement(character, position))
                    {
                        character.FacingDirection = character.MovingDirection;
                    }
                }
            }
        }
    }
}
