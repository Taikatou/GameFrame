using System;
using GameFrame.Controllers;
using GameFrame.ServiceLocator;

namespace Demos.DesktopGl
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            StaticServiceLocator.AddService<IControllerSettings>(new ControllerSettings());
            using (var game = new DemoGame())
                game.Run();
        }
    }
}
