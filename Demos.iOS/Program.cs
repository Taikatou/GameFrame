using Foundation;
using HockeyApp.iOS;
using UIKit;
using Demos.Common;
using Demos.MobileShared;
using GameFrame.Controllers;
using GameFrame.ServiceLocator;

namespace Demos.iOS
{
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static DemoGame _game;

        internal static void RunGame()
        {
            StaticServiceLocator.AddService<IControllerSettings>(new ControllerSettings());
            _game = new DemoGame();
            _game.Run();
        }

        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        public override void FinishedLaunching(UIApplication app)
        {
            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure(IdManager.HockeyAppId);
            manager.StartManager();
            manager.Authenticator.AuthenticateInstallation();
            RunGame();
        }
    }
}
