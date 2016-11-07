using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonoGame.Extended.Content.Pipeline;

namespace GameFrame.ContentPipeline.Tests
{
    [TestClass]
    public class InkStoryProcessorTest
    {
        [TestMethod]
        public void TestProccessor()
        {
            var filePath = PathExtensions.GetApplicationFullPath(@"TestData\hello.ink.json");
            var importer = new InkImporter();
            var importerResult = importer.Import(filePath, null);

            var processor = new InkProcessor();
            var result = processor.Process(importerResult, null);
            Debug.WriteLine(result);
        }
    }
}
