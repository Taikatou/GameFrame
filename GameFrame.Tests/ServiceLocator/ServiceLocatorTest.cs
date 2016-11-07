using System;
using Demos.DesktopGl;
using GameFrame.Controllers;
using GameFrame.ServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameFrame.Tests.ServiceLocator
{
    [TestClass]
    public class ServiceLocatorTest
    {
        [TestMethod]
        public void AddAndGetServiceTest()
        {
            StaticServiceLocator.AddService<IControllerSettings>(new ControllerSettings());
            var settings = StaticServiceLocator.GetService<IControllerSettings>();
            Assert.IsInstanceOfType(settings,typeof(IControllerSettings));
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
