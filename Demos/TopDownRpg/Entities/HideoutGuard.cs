using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class HideoutGuard : AbsractBattleEntity
    {
        public HideoutGuard(GameModeController gameModeController, string flag, Vector2 startPosition, Vector2 endPosition) : base(gameModeController, flag, startPosition, endPosition)
        {
            Name = "Guard";
            SpriteSheet = "1";
        }

        public override GameFrameStory Interact()
        {
            GameStory = ReadStory("hideout_guard.ink");
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
            return GameStory;
        }
    }
}
