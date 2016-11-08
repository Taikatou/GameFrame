using Microsoft.Xna.Framework.Content;
using Ink.Runtime;

namespace GameFrame.ContentPipeline
{
    public class InkReader : ContentTypeReader<Story>
    {
        protected override Story Read(ContentReader input, Story existingInstance)
        {
            var text = input.ReadString();
            return new Story(text);
        }
    }
}
