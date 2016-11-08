using System;
using System.Collections.Generic;
using GameFrame.Controllers;
using GameFrame.Ink;
using GameFrame.Interceptor;
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
            StaticServiceLocator.AddService(new List<IInterceptor<StoryContext>> { new TextToSpeechStoryInterceptor()});

            using (var game = new DemoGame())
            {
                game.Run();
            }
        }
    }
}