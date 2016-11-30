using System;
using GameFrame.Controllers;
using GameFrame.PathFinding.PossibleMovements;
using GameFrame.ServiceLocator;
using GameFrame.Services;
using GameFrame.TextToSpeech;

namespace Demos.DesktopGl
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            StaticServiceLocator.AddService<ISaveAndLoad>(new SaveAndLoad());
            StaticServiceLocator.AddService<IControllerSettings>(new ControllerSettings());
            StaticServiceLocator.AddService<ITextToSpeech>(new TextToSpeechImplementation());
            StaticServiceLocator.AddService<IPossibleMovements>(new FourWayPossibleMovement());

            using (var game = new DemoGame("courier-new-16"))
            {
                game.Run();
            }
        }
    }
}