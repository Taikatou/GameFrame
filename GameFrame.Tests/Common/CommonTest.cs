using System;
using GameFrame.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using NUnit.Framework;
using MonoGame.Extended.ViewportAdapters;
using Assert = NUnit.Framework.Assert;

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
        [ExpectedException(typeof(System.Exception))]
        public void TestCameraUpdate()
        {
            //Camera2D Camera2D = null;
            //IFocusAble focusAble = null;
            //var tracker = new CameraTracker(Camera2D, focusAble);
            //var time = new GameTime(TimeSpan.MaxValue, TimeSpan.Zero);
            //tracker.Update(time);
        }

        [Test]
        public void StringToVectorTest()
        {

        }
    }
}
