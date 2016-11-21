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
            var storyText = StoryImporter.ReadStory(Script);
            var story = new GameFrameStory(storyText);
            return story;
        }

        public virtual void CompleteInteract()
        {
            
        }
    }
}
