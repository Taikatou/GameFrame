using Demos.TopDownRpg.SpeedState;
using GameFrame.Ink;
using GameFrame.Movers;
using GameFrame.ServiceLocator;
using GameFrame.Services;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Demos.TopDownRpg
{
    public class Entity : BaseMovable
    {
        [JsonProperty("name")] public string Name;

        [JsonProperty("sprite-sheet")] public string SpriteSheet;

        [JsonProperty("script")] public string Script;

        private GameFrameStory _story;

        public SpeedContext SpeedContext;
        public override float Speed => SpeedContext.Speed;

        [JsonConstructor]
        public Entity(string name, string spriteSheet, string script)
        {
            Name = name;
            SpriteSheet = spriteSheet;
            Script = script;
            SpeedContext = new SpeedContext(4);
        }

        public Entity(Entity baseEntity, Vector2 position)
        {
            Name = baseEntity.Name;
            SpriteSheet = baseEntity.SpriteSheet;
            Script = baseEntity.Script;
            SpeedContext = new SpeedContext(4);
            FacingDirection = baseEntity.FacingDirection;
            Position = position;
        }

        public virtual GameFrameStory Interact()
        {
            if (_story == null && !string.IsNullOrEmpty(Script))
            {
                var storyText = StoryImporter.ReadStory(Script);
                _story = new GameFrameStory(storyText);
            }
            return _story;
        }

        public static Entity Import(string fileName)
        {
            var textReader = StaticServiceLocator.GetService<ISaveAndLoad>();
            var jsonText = textReader.LoadText($"Entities/{fileName}.json");
            var entity = JsonConvert.DeserializeObject<Entity>(jsonText);
            return entity;
        }

        public virtual void CompleteInteract()
        {
            
        }
    }
}
