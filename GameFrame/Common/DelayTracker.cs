using GameFrame.Movers;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Common
{
    public class DelayTracker : IUpdate
    {
        private Vector2 _cachedPosition;
        private readonly BaseMovable _follower;
        private readonly BaseMovable _following;
        public DelayTracker(BaseMovable follower, BaseMovable following)
        {
            _follower = follower;
            _following = following;
            _cachedPosition = following.Position;
        }

        public void Update(GameTime gameTime)
        {
            if (_cachedPosition != _following.Position)
            {
                _follower.Moving = true;
                _follower.MovingDirection = _following.Position - _follower.Position;
                _cachedPosition = _following.Position;
            }
            else if (_follower.Position == _cachedPosition && _follower.Moving)
            {
                _follower.Moving = false;
            }
        }
    }
}
