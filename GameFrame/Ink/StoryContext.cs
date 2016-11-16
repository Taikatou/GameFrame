using System.Collections.Generic;
using GameFrame.Interceptor;
using Ink.Runtime;

namespace GameFrame.Ink
{
    public class StoryContext : IContext
    {
        public string Text;
        private readonly Story _story;
        public List<Choice> Choices => _story.currentChoices;

        public StoryContext(Story story)
        {
            _story = story;
            Text = story.currentText;
        }

        public StoryContext(Story story, string storyText)
        {
            _story = story;
            storyText = storyText.Replace(System.Environment.NewLine, "");
            Text = storyText;
        }
    }
}
