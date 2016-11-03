using GameFrame.Paths;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace GameFrame.Tests.Paths
{
    [TestClass]
    public class PathTest
    {
        public AbstractPath Path;

        [TestMethod]
        public void PathsTest()
        {
            Path = new FinitePath();
        }
    }
}
