using GameFrame.CollisionSystems;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public class PlayerMover : IMover
    {
        private ICollisionSystem _collisionSystem;
        private IMovable _playerCharacter;

        public PlayerMover(ICollisionSystem collisionSystem, IMovable playerCharacter)
        {
            _collisionSystem = collisionSystem;
            _playerCharacter = playerCharacter;
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}
