using GameFrame.Ink;

namespace Demos.TopDownRpg.Entities
{
    public abstract class AbstractCompleteEntity : MovingEntity
    {
        public bool StoryOver;
        public GameFrameStory GameStory;
        public abstract GameFrameStory StoryScript { get; }
        public abstract GameFrameStory CompleteScript { get; }

        public override GameFrameStory Interact()
        {
            GameStory = !StoryOver ? StoryScript : CompleteScript;
            return GameStory;
        }

        public override void CompleteInteract()
        {
            if (!StoryOver && GameStory != null)
            {
                StoryOver = GameStory.GetVariableState<int>("story_over") == 1;
            }
        }
    }
}
