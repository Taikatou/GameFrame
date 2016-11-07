using GameFrame.Ink;
using GameFrame.Interceptor;
using GameFrame.ServiceLocator;
using GameFrame.TextToSpeech;

namespace Demos.DesktopGl
{
    public class TextToSpeechStoryInterceptor : IInterceptor<StoryContext>
    {
        public void Execute(StoryContext context)
        {
            var textToSpeech = StaticServiceLocator.GetService<ITextToSpeech>();
            textToSpeech.Speak(context.Text);
        }
    }
}
