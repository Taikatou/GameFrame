using GameFrame.Interceptor;
using Ink.Runtime;

namespace GameFrame.Ink
{
    public class StoryContext : IContext
    {
        public string Text;

        public StoryContext(Story story)
        {
            Text = story.currentText;
        }
    }
}
