using Ink.Runtime;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace GameFrame.ContentPipeline
{
    [ContentTypeWriter]
    public class InkWriter : ContentTypeWriter<string>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(Story).AssemblyQualifiedName;
        }

        protected override void Write(ContentWriter output, string value)
        {
            output.Write(value);
        }
    }
}
