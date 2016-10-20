using System.Collections.Generic;
using GameFrame.CollisionSystems;
using GameFrame.CollisionSystems.SpatialHash;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public class SpatialHashMoverManager<T> : IMoverManager<T>, IUpdate where T : BaseMovable
    {
        private readonly ICollisionSystem _collisionSystem;
        private readonly ExpiringSpatialHashCollisionSystem<T> _spatialHashLayer;
        private readonly List<T> _characterList;

        public SpatialHashMoverManager(ICollisionSystem collisionSystem, ExpiringSpatialHashCollisionSystem<T> spatialHashLayer)
        {
            _collisionSystem = collisionSystem;
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
            if (!_collisionSystem.CheckCollision((int)position.X, (int)position.Y))
            {
                //William timer
                if(_spatialHashLayer.MoveNode(character.Position.ToPoint(), position.ToPoint(), character.Speed))
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
