using GameFrame.ServiceLocator;
using GameFrame.Services;

namespace GameFrame.Ink
{
    public class StoryImporter
    {
        public static string ReadStory(string textFile)
        {
            var textReader = StaticServiceLocator.GetService<ISaveAndLoad>();
            var storyText = textReader.LoadText($"Scripts/{textFile}.json");
            return storyText;
        }
    }
}
