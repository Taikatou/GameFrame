using System.Diagnostics;
using GameFrame.Movers;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Common
{
    public class DelayTracker : IUpdate
    {
        private Vector2 _cachedPosition;
        private Vector2 _endPosition;
        private readonly BaseMovable _follower;
        private readonly BaseMovable _following;
        public DelayTracker(BaseMovable follower, BaseMovable following)
        {
            _follower = follower;
            _following = following;
            _cachedPosition = following.Position;
            _endPosition = following.Position;
        }

        public void Update(GameTime gameTime)
        {
            var followingMoved = _cachedPosition != _following.Position;
            if (followingMoved && !_follower.Moving)
            {
                if (Distance.GetDistance(_follower.Position, _following.Position) >
                    Distance.GetDistance(_cachedPosition, _following.Position))
                {
                    Move();
                }
                _cachedPosition = _following.Position;
            }
            else if (_follower.Position == _endPosition && _follower.Moving)
            {
                _follower.Moving = false;
            }
        }

        public void Move()
        {
            _follower.Moving = true;
            _endPosition = _cachedPosition;
            _follower.MovingDirection = _endPosition - _follower.Position;
        }
    }
}
