using GameFrame.Ink;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace Demos.TopDownRpg.Entities
{
    public delegate void Move(Entity entity, Point p);
    public class NpcEntity : Entity
    {
        public Move MoveDelegate { internal get; set; }

        public NpcEntity()
        {
            
        }
        [JsonConstructor]
        public NpcEntity(string name, string spriteSheet, string script)
        {
            Name = name;
            SpriteSheet = spriteSheet;
            Script = script;
        }

        public virtual GameFrameStory ReadStory(string storyName)
        {
            var storyText = StoryImporter.ReadStory(storyName);
            var story = new GameFrameStory(storyText);
            return story;
        }
    }
}
