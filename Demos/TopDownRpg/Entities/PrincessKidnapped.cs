using Demos.TopDownRpg.GameModes;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class PrincessKidnapped : Princess
    {
        private GameFrameStory _gameStory;
        private readonly Teleport _teleport;
        private readonly Say _say;
        public PrincessKidnapped(Teleport teleport, Say say)
        {
            _teleport = teleport;
            _say = say;
        }
        public override GameFrameStory Interact()
        {
            var scriptName = Flags.MasterDefeated ? "princess_saved.ink" : "princess_kidnapped.ink";
            _gameStory = ReadStory(scriptName);
            return _gameStory;
        }

        public override void CompleteInteract()
        {
            if (Flags.MasterDefeated)
            {
                Flags.GameComplete = true;
                var player = PlayerEntity.Instance;
                player.Position = new Vector2(9, 8);
                player.FacingDirection = new Vector2(0, 1);
                _teleport.Invoke("perfect_house");
                var story = ReadStory("conclusion.ink");
                story.Continue();
                _say.Invoke(story);
            }
        }
    }
}
