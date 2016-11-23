using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class NorthDesertGuard : SwitchNpcEntity
    {
        private GameFrameStory _gameStory;
        public NorthDesertGuard(string flag, Vector2 startPosition, Vector2 endPosition) : base(flag, startPosition, endPosition)
        {
            Name = "Guard";
            SpriteSheet = "1";
        }

        public override GameFrameStory Interact()
        {
            var learnedFight = GameFlags.GetFlag<bool>("learned_fight");
            var scriptName = learnedFight ? "north_guard_post_fight" : "north_guard_pre_fight";
            _gameStory = ReadStory(scriptName + ".ink");
            return _gameStory;
        }

        public override void CompleteInteract()
        {
            
        }
    }
}
