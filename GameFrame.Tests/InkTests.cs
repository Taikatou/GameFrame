using System.Diagnostics;
using Ink.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameFrame.Tests
{
    [TestClass]
    public class InkTests
    {
        [TestMethod]
        public void TestInk()
        {
            // 1) Load story
            var story = new Story("");

            // 2) Game content, line by line
            while (story.canContinue)
                Debug.WriteLine(story.Continue());

            // 3) Display story.currentChoices list, allow player to choose one
            Debug.WriteLine(story.currentChoices[0].text);
            story.ChooseChoiceIndex(0);

            // 4) Back to 2
        }
    }
}
