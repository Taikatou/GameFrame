using System;
using GameFrame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonoGame.Extended;

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

        [TestMethod]
        public void TestCameraUpdate()
        {
            Camera2D Camera2D = null;
            IFocusAble focusAble = null;
            var tracker = new CameraTracker(Camera2D, focusAble);
            tracker.Update(null);
            Assert.IsNull(tracker);
        }
    }
}
