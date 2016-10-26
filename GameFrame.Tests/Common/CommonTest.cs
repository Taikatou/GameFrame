using GameFrame.Common;
using MonoGame.Extended;
using NUnit.Framework;

namespace GameFrame.Tests.Common
{
    [TestFixture]
    public class CommonTest
    {
        [Test]
        public void TestCameraTracker()
        {
            Camera2D Camera2D = null;
            IFocusAble focusAble = null;
            var tracker = new CameraTracker(Camera2D, focusAble);
            Assert.IsNotNull(tracker);
        }

        [Test]
        public void TestCameraUpdate()
        {
            Camera2D Camera2D = null;
            IFocusAble focusAble = null;
            var tracker = new CameraTracker(Camera2D, focusAble);
            tracker.Update(null);
            //Assert.IsNull(tracker);
        }
    }
}
