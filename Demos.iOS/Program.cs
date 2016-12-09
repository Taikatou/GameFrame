using System;
using Demos.MobileShared;
using Foundation;
using GameFrame.Controllers;
using GameFrame.PathFinding.Heuristics;
using GameFrame.PathFinding.PossibleMovements;
using GameFrame.ServiceLocator;
using GameFrame.Services;
using UIKit;

namespace Demos.iOS
{
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static DemoGame game;

        internal static void RunGame()
        {
            StaticServiceLocator.AddService<ISaveAndLoad>(new SaveAndLoad());
            StaticServiceLocator.AddService<IControllerSettings>(new ControllerSettings());
            StaticServiceLocator.AddService<IPossibleMovements>(new EightWayPossibleMovement(new CrowDistance()));
            game = new DemoGame();
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
    }
}
