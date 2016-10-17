using GameFrame.CollisionSystems;
using GameFrame.CollisionSystems.SpatialHash;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public class SpatialHashMoverManager<T> : IMoverManager, IUpdate where T : BaseMovable
    {
        private readonly ICollisionSystem _collisionSystem;
        private readonly ExpiringSpatialHashCollisionSystem<T> _spatialHashLayer;
        private readonly T _playerCharacter;

        public SpatialHashMoverManager(ICollisionSystem collisionSystem, T playerCharacter, ExpiringSpatialHashCollisionSystem<T> spatialHashLayer)
        {
            _collisionSystem = collisionSystem;
            _playerCharacter = playerCharacter;
            _spatialHashLayer = spatialHashLayer;
            _spatialHashLayer.AddNode(_playerCharacter.Position.ToPoint(), _playerCharacter);
        }

        public bool RequestMovement(Vector2 position)
        {
            if (!_collisionSystem.CheckCollision((int)position.X, (int)position.Y))
            {
                if(_spatialHashLayer.MoveNode(_playerCharacter.Position.ToPoint(), position.ToPoint(), 200))
                {
                    _playerCharacter.Position = position;
                    return true;
                }
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (_playerCharacter.Moving)
            {
                var position = _playerCharacter.Position + _playerCharacter.MovingDirection;
                if (RequestMovement(position))
                {
                    _playerCharacter.FacingDirection = _playerCharacter.MovingDirection;
                }
            }
        }
    }
}
