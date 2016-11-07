using GameFrame.Interceptor;
using Ink.Runtime;

namespace GameFrame.Ink
{
    public class StoryContext : IContext
    {
        public string Text => _story.currentText;
        private Story _story;

        public StoryContext(Story story)
        {
            _story = story;
        }
    }
}
