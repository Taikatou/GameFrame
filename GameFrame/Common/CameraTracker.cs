using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Common
{
    public class CameraTracker : IUpdate
    {
        private readonly Camera2D _follower;
        private readonly IMovable _following;

        public CameraTracker(Camera2D follower, IMovable following)
        {
            _follower = follower;
            _following = following;
        }

        public void Update(GameTime gameTime)
        {
            _follower.LookAt(_following.Position);
        }
    }
}
