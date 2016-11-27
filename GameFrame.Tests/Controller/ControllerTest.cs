using System.Collections.Generic;
using Demos.DesktopGl;
using GameFrame.Common;
using GameFrame.Controllers;
using GameFrame.Controllers.KeyBoard;
using GameFrame.Controllers.SmartButton;
using GameFrame.Movers;
using GameFrame.PathFinding.Heuristics;
using GameFrame.PathFinding.PossibleMovements;
using GameFrame.ServiceLocator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Tests.Controller
{
    [TestClass]
    public class ControllerTest
    {
        public static IServiceLocator Instance;

        [TestMethod]
        public void SinglePLayerControllerFactoryTest()
        {
            StaticServiceLocator.AddService<IControllerSettings>(new ControllerSettings());
            var controller = new SinglePlayerControllerFactory();
            controller.CreateEntityController(new BaseMovable(), new EightWayPossibleMovement(new CrowDistance()),
                new MoverManager());
            const Buttons buttons = Buttons.A;
            controller.AddGamePadButton(new List<IButtonAble>(), buttons);
            controller.AddKeyBoardButton(new List<IButtonAble>(), Keys.A);

            var button = new KeyButton(Keys.A);


            var smart = new SmartController();
            smart.AddButton(new BaseSmartButton(button));
            smart.Update(new GameTime());
        }

        [TestMethod]
        public void ControllersClickTest()
        {
           
        }
    }
}