using System;
using GameFrame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace GameFrame.Tests.Common
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void TestCameraTracker()
        {
            Camera2D Camera2D = null;
            IFocusAble focusAble = null;
            var tracker = new CameraTracker(Camera2D, focusAble);
            Assert.IsNotNull(tracker);
        }
    }
}
