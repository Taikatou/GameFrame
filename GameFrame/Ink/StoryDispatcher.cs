using GameFrame.Interceptor;
using Ink.Runtime;

namespace GameFrame.Ink
{
    public class StoryDispatcher : Dispatcher<StoryContext>
    {
        public void AddStory(Story story)
        {
            var storyStripped = new StoryContext(story);
            Execute(storyStripped);
        }

        public void AddStory(Story story, string storyText)
        {
            var storyStripped = new StoryContext(story, storyText);
            Execute(storyStripped);
        }

        public void StoryText(Story story, string text)
        {
            var storyStripped = new StoryContext(story, text);
            Execute(storyStripped);
        }
    }
}
