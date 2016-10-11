using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Common
{
    public class CameraTracker : IUpdate
    {
        private readonly Camera2D _follower;
        private readonly IFocusAble _following;
        private Point _cachedPosition;

        public CameraTracker(Camera2D follower, IFocusAble following)
        {
            _follower = follower;
            _following = following;
        }

        public void Update(GameTime gameTime)
        {
            if (_cachedPosition != _following.ScreenPosition)
            {
                var focusOn = _following.ScreenPosition + _following.Offset;
                _follower.LookAt(focusOn.ToVector2());
                _cachedPosition = _following.ScreenPosition;
            }
        }
    }
}
