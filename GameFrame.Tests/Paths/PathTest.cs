using System.Collections.Generic;
using GameFrame.Paths;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
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
            Path = new FinitePath(new List<Point>());
        }
    }
}
