using System.ServiceModel;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class PlayerEntity : Entity
    {
        public static PlayerEntity Instance { get; set; }

        public PlayerEntity()
        {
            Name = "Player";
            SpriteSheet = "Character";
        }

        public PlayerEntity(Vector2 position)
        {
            Name = Instance.Name;
            SpriteSheet = Instance.SpriteSheet;
            Script = Instance.Script;
            FacingDirection = Instance.FacingDirection;
            Position = position;
        }
    }
}
