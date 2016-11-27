using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class NorthDesertGuard : AbsractBattleEntity
    {
        public NorthDesertGuard(GameModeController gameModeController, string flag, Vector2 startPosition, Vector2 endPosition) : base(gameModeController, flag, startPosition, endPosition)
        {
            Name = "Guard";
            SpriteSheet = "1";
        }

        public override GameFrameStory Interact()
        {
            var learnedFight = GameFlags.GetVariable<bool>("learned_fight");
            var scriptName = learnedFight ? "north_guard_post_fight.ink" : "north_guard_pre_fight.ink";
            GameStory = ReadStory(scriptName);
            if(learnedFight)
            {
                GameStory.ChoosePathString("dialog");
                CompleteEvent completeEvent = win =>
                {
                    if (win)
                    {
                        MoveDelegate?.Invoke(this, EndPosition.ToPoint());
                        GameFlags.SetVariable(FlagName, true);
                        AlreadyMoved = true;
                    }
                };
                ReadStory(GameStory, completeEvent);
            }
            return GameStory;
        }
    }
}
