using System.IO;
using Demos.TopDownRpg.SpeedState;
using GameFrame.Ink;
using GameFrame.Movers;
using GameFrame.ServiceLocator;
using GameFrame.Services;
using Ink.Runtime;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Demos.TopDownRpg
{
    public class Entity : BaseMovable
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("sprite-sheet")]
        public string SpriteSheet;

        [JsonProperty("script")]
        public string Script;

        public SpeedContext SpeedContext;
        public override float Speed => SpeedContext.Speed;

        public Entity(string name, string spriteSheet, string script)
        {
            Name = name;
            SpriteSheet = spriteSheet;
            Script = script;
            SpeedContext = new SpeedContext(4);
        }

        public virtual Story Interact()
        {
            Story story = null;
            if (!string.IsNullOrEmpty(Script))
            {
                story = StoryImporter.ReadStory(Script);
            }
            return story;
        }

        public static Entity Import(string fileName)
        {
            var textReader = StaticServiceLocator.GetService<ISaveAndLoad>();
            var jsonText = textReader.LoadText($"Entities/{fileName}.json");
            var entity = JsonConvert.DeserializeObject<Entity>(jsonText);
            return entity;
        }
    }
}
