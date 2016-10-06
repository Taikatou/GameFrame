using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Common
{
    public class CameraTracker : IUpdate
    {
        private readonly Camera2D _follower;
        private readonly IMovable _following;
        private Vector2 _cachedPosition;

        public CameraTracker(Camera2D follower, IMovable following)
        {
            _follower = follower;
            _following = following;
        }

        public void Update(GameTime gameTime)
        {
            if (_cachedPosition != _following.Position)
            {
                _follower.LookAt(_following.Position);
                _cachedPosition = _following.Position;
            }
        }
    }
}
