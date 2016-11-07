using GameFrame.ServiceLocator;
using GameFrame.Services;
using Ink.Runtime;

namespace GameFrame.Ink
{
    public class StoryImporter
    {
        public static Story ReadStory(string textFile)
        {
            var textReader = StaticServiceLocator.GetService<ISaveAndLoad>();
            var storyText = textReader.LoadText($"Scripts/{textFile}.json");
            return new Story(storyText);
        }
    }
}
