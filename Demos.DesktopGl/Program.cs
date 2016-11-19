using System;
using GameFrame.Controllers;
using GameFrame.ServiceLocator;
using GameFrame.Services;
using GameFrame.TextToSpeech;
using GameFrame.Ink;
using System.Collections.Generic;

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
            //StaticServiceLocator.AddService(new List<StoryInterceptor> { new TextToSpeechStoryInterceptor()});

            using (var game = new DemoGame())
            {
                game.Run();
            }
        }
    }
}