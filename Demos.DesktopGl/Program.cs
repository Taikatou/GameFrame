using System;
using GameFrame.Controllers;
using GameFrame.ServiceLocator;
using GameFrame.Services;

namespace Demos.DesktopGl
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            StaticServiceLocator.AddService<ISaveAndLoad>(new SaveAndLoad());
            StaticServiceLocator.AddService<IControllerSettings>(new ControllerSettings());
            using (var game = new DemoGame())
                game.Run();
        }
    }
}
