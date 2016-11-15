using GameFrame.Ink;
using GameFrame.ServiceLocator;
using GameFrame.TextToSpeech;

namespace Demos.Droid
{
    public class TextToSpeechStoryInterceptor : StoryInterceptor
    {
        public override void Execute(StoryContext context)
        {
            var textToSpeech = StaticServiceLocator.GetService<ITextToSpeech>();
            textToSpeech.Speak(context.Text);
        }
    }
}
