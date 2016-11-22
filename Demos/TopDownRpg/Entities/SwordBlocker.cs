using System;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class SwordBlocker : NpcEntity
    {
        private readonly GameFlags _gameFlags;
        private bool _complete;
        private bool _moved;

        public SwordBlocker(GameFlags gameFlags)
        {
            Name = "Concerned country man";
            SpriteSheet = "3";
            _gameFlags = gameFlags;
        }

        public override GameFrameStory Interact()
        {
            var acquiredSword = _gameFlags.GetFlag<bool>("acquire_sword");
            GameFrameStory toReturn = null;
            if (acquiredSword)
            {
                _complete = true;
                toReturn = ReadStory("sword_blocker_complete.ink");
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
