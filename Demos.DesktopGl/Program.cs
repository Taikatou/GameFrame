using System;
using GameFrame.Controllers;
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

            ITextToSpeech speech = new TextToSpeechImplementation();
            speech.Speak("Welcome to GameFrame");

            using (var game = new DemoGame())
                game.Run();
        }
    }
}
