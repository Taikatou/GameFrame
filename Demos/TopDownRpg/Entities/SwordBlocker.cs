using System;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class SwordBlocker : NpcEntity
    {
        private bool _complete;
        private bool _moved;

        public SwordBlocker()
        {
            Name = "Concerned country man";
            SpriteSheet = "3";
        }

        public override GameFrameStory Interact()
        {
            var acquiredSword = GameFlags.GetFlag<bool>("acquire_sword");
            GameFrameStory toReturn = null;
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
            if (_complete && !_moved)
            {
                MoveDelegate.Invoke(this, new Point(2, 20));
                _moved = true;
            }
        }
    }
}
