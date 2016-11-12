﻿using System;
using Demos.DesktopGl;
using GameFrame.Common;
using GameFrame.Controllers;
using GameFrame.Controllers.KeyBoard;
using GameFrame.ServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Tests.ServiceLocator
{
    [TestClass]
    public class ServiceLocatorTest
    {
        [TestMethod]
        public void AddAndGetServiceTest()
        {
            var keyBoardUpdator = new KeyboardUpdater();
            StaticServiceLocator.AddService<IButtonAble>(new KeyButton(Keys.A, keyBoardUpdator));
            var settings = StaticServiceLocator.GetService<IButtonAble>();
            Assert.IsInstanceOfType(settings,typeof(IButtonAble));
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception), "The requested service is not registered")]
        public void AddAndGetBadServiceTest()
        {
            var settings = StaticServiceLocator.GetService<IControllerSettings>();
            Assert.IsNotInstanceOfType(settings, typeof(IControllerSettings));
        }
    }
}
