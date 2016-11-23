using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class SwordBlocker : SwitchNpcEntity
    {
        private bool _complete;
        private bool _moved;

        public SwordBlocker(string flag, Vector2 startPosition, Vector2 endPosition) : base(flag, startPosition, endPosition)
        {
            Name = "Concerned country man";
            SpriteSheet = "3";
        }

        public override GameFrameStory Interact()
        {
            var acquiredSword = GameFlags.GetFlag<bool>("acquire_sword");
            GameFrameStory toReturn;
            if (acquiredSword)
            {
                _complete = true;
                toReturn = ReadStory("sword_blocker_complete.ink");
                GameFlags.AddObject("sword_blocker_moved", true);
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
                MoveDelegate.Invoke(this, new Point(2, 20));
                _moved = true;
            }
        }
    }
}
