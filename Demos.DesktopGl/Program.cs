using System;

namespace Demos.DesktopGl
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var game = new Game1();
            //using (var game = new Game1())
              game.Run();
        }
    }
}
