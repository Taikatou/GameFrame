using System;

namespace Demos.DesktopGl
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new DemoGame())
                game.Run();
        }
    }
}
