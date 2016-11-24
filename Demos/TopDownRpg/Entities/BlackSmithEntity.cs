using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class BlackSmithEntity : AbstractCompleteEntity
    {
        public override GameFrameStory StoryScript => ReadStory("black_smith.ink");
        public override GameFrameStory CompleteScript => ReadStory("black_smith_complete.ink");
        public bool AcquireSword;

        public BlackSmithEntity()
        {
            Name = "Dojo Master";
            SpriteSheet = "2";
        }

        public override GameFrameStory Interact()
        {
            if (StoryOver && !AcquireSword)
            {
                AcquireSword = true;
                GameFlags.SetVariable("acquire_sword", true);
                return ReadStory("dojo_master_acquire_sword.ink");
            }
            else
            {
                return base.Interact();
            }
        }

        public override void CompleteInteract()
        {
            base.CompleteInteract();
            if (StoryOver && !AcquireSword)
            {
                MoveDelegate?.Invoke(this, new Point(6, 3));
            }
        }
    }
}
