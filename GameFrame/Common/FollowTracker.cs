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
            Debug.WriteLine("Moving " + _follower.Position + " to " + _following.Position);
            _follower.Position = _following.Position;
            Debug.WriteLine("Moved " + _follower.Position + " to " + _following.Position);
        }
    }
}
