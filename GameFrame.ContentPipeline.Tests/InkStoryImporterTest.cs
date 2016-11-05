using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonoGame.Extended.Content.Pipeline;

namespace GameFrame.ContentPipeline.Tests
{
    [TestClass]
    public class InkStoryImporterTest
    {
        [TestMethod]
        public void TestImporter()
        {
            var filePath = PathExtensions.GetApplicationFullPath(@"TestData\hello.ink.json");
            var importer = new InkImporter();
            var result = importer.Import(filePath, null);
        }
    }
}
