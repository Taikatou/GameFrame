using Foundation;
using UIKit;

namespace Demos.iOS
{
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static Game1 _game;

        internal static void RunGame()
        {
            _game = new Game1();
            _game.Run();
        }

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
