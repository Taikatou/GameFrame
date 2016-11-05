﻿using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Common
{
    public class CameraTracker : IUpdate
    {
        private readonly Camera2D _follower;
        private readonly IFocusAble _following;
        private Vector2 _cachedPosition;

        public CameraTracker(Camera2D follower, IFocusAble following)
        {
            _follower = follower;
            _following = following;
        }

        public void Update(GameTime gameTime)
        {
            if (_cachedPosition != _following.Position)
            {
                var focusOn = _following.Position + _following.Offset;
                _follower.LookAt(focusOn);
                _cachedPosition = _following.Position;
            }
        }
    }
}
