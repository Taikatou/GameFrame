using GameFrame.CollisionSystems;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public class PlayerMover : IMover
    {
        private readonly ICollisionSystem _collisionSystem;
        private readonly IMovable _playerCharacter;

        public PlayerMover(ICollisionSystem collisionSystem, IMovable playerCharacter)
        {
            _collisionSystem = collisionSystem;
            _playerCharacter = playerCharacter;
        }

        public void RequestMovement(Vector2 position)
        {
            if (!_collisionSystem.CheckCollision((int)position.X, (int)position.Y))
            {
                _playerCharacter.Position = position;
            }
        }
    }
}
