using Microsoft.Xna.Framework.Content.Pipeline;

namespace GameFrame.ContentPipeline
{
    [ContentProcessor(DisplayName = "GameFrame.ContentPipeline.InkProcessor")]
    public class InkProcessor : ContentProcessor<string, string>
    {
        public override string Process(string input, ContentProcessorContext context)
        {
            return input;
        }
    }
}