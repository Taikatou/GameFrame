using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace GameFrame.ContentPipeline
{

    [ContentImporter(".ink.json", DisplayName = "Ink Importer", DefaultProcessor = "InkProcessor")]
    public class InkImporter : ContentImporter<string>
    {
        public override string Import(string filename, ContentImporterContext context)
        {
            //context.Logger.LogMessage("Importing ink Story {0}", filename);
            using (var file = File.OpenText(filename))
            {
                var storyText = file.ReadToEnd();
                return storyText;
            }
        }
    }
}
