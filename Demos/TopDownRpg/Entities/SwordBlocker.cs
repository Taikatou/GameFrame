using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class SwordBlocker : SwitchNpcEntity
    {
        private bool _complete;
        private bool _moved;
        private readonly Collision _collision;

        public SwordBlocker(string flag, Vector2 startPosition, Vector2 endPosition, Collision collision) : base(flag, startPosition, endPosition)
        {
            Name = "Concerned country man";
            SpriteSheet = "3";
            _collision = collision;
        }

        public override GameFrameStory Interact()
        {
            GameFrameStory toReturn;
            if (Flags.AcquiredSword)
            {
                _complete = true;
                toReturn = ReadStory("sword_blocker_complete.ink");
                GameFlags.SetVariable("sword_blocker_moved", true);
            }
            else if(!_moved)
            {
                toReturn = ReadStory("sword_blocker.ink");
            }
            else
            {
                toReturn = ReadStory("sword_blocker_moved.ink");
            }
            return toReturn;
        }

        public override void CompleteInteract()
        {
            if (_complete && !_moved && !AlreadyMoved)
            {
                var collision = _collision.Invoke(Position.ToPoint(), new Point(2, 20));
                var endPoint = collision ? new Point(2, 22) : new Point(2, 20);
                MoveDelegate.Invoke(this, endPoint);
                _moved = true;
            }
        }
    }
}
