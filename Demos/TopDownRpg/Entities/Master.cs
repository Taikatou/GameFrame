using GameFrame.Ink;

namespace Demos.TopDownRpg.Entities
{
    public class Master : AbstractCompleteEntity
    {
        public override GameFrameStory StoryScript => ReadStory("master_pre_learn_fight.ink");
        public override GameFrameStory CompleteScript => ReadStory("master_complete.ink");

        public Master()
        {
            Name = "Master";
            SpriteSheet = "5";
        }

        public override void CompleteInteract()
        {
            var preStoryOver = StoryOver;
            base.CompleteInteract();
            if (StoryOver && !preStoryOver)
            {
                GameFlags.AddObject("learned_fight", true);
            }
        }
    }
}
