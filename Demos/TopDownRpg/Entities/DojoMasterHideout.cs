using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class DojoMasterHideout : AbsractBattleEntity
    {
        private GameFrameStory _gameStory;
        public DojoMasterHideout(GameModeController gameModeController, string flag, Vector2 startPosition, Vector2 endPosition) : base(gameModeController, flag, startPosition, endPosition)
        {
            Name = "Dojo Master";
            SpriteSheet = "1";
        }
        public override GameFrameStory Interact()
        {
            if (!Flags.MasterDefeated)
            {
                _gameStory = ReadStory("dojo_master_hideout.ink");
                CompleteEvent completeEvent = win =>
                {
                    Flags.MasterDefeated = true;
                };
                ReadStory(_gameStory, completeEvent);
            }
            else
            {
                _gameStory = ReadStory("dojo_master_defeated.ink");
            }
            return _gameStory;
        }

        public override void CompleteInteract()
        {
            
        }
    }
}
