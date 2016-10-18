using System.Diagnostics;
using MonoGame.Extended;
using Microsoft.Xna.Framework;

namespace GameFrame.Common
{
    public class FollowTracker : IUpdate
    {
        private readonly IMovable _follower;
        private readonly IMovable _following;

        public FollowTracker(IMovable follower, IMovable following)
        {
            _follower = follower;
            _following = following;
        }

        public void Update(GameTime gameTime)
        {
            _follower.Position = _following.Position;
        }
    }
}
