using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class DojoMasterHideout : AbsractBattleEntity
    {
        public DojoMasterHideout(GameModeController gameModeController, string flag, Vector2 startPosition, Vector2 endPosition) : base(gameModeController, flag, startPosition, endPosition)
        {
            Name = "Dojo Master";
            SpriteSheet = "1";
        }
        public override GameFrameStory Interact()
        {
            var story = ReadStory("dojo_master_hideout.ink");
            CompleteEvent completeEvent = win =>
            {

            };
            ReadStory(story, completeEvent);
            return story;
        }
    }
}
