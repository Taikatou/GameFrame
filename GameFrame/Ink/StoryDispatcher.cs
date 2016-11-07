using GameFrame.Interceptor;
using Ink.Runtime;

namespace GameFrame.Ink
{
    public class StoryDispatcher : Dispatcher<StoryContext>
    {
        public void AddStory(Story story)
        {
            while (story.canContinue)
            {
                story.Continue();
                var storyStripped = new StoryContext(story);
                Execute(storyStripped);
            }
        }
    }
}
