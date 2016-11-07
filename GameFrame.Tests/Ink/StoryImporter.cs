using System.Diagnostics;
using Demos.MobileShared;
using GameFrame.ServiceLocator;
using GameFrame.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameFrame.Tests.Ink
{
    [TestClass]
    public class StoryImporter
    {
        [TestMethod]
        public void ImportStory()
        {
            StaticServiceLocator.AddService<ISaveAndLoad>(new SaveAndLoad());
            var textReader = StaticServiceLocator.GetService<ISaveAndLoad>();
            var storyText = textReader.LoadText("Scripts/hello.ink.json");
            Debug.WriteLine(storyText);
        }
    }
}
