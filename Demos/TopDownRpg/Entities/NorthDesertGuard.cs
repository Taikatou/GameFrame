using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class NorthDesertGuard : AbsractBattleEntity
    {
        private readonly Vector2 _alternativeEndPoint;
        private readonly Collision _collision;
        public NorthDesertGuard(GameModeController gameModeController, string flag, Vector2 startPosition, Vector2 endPosition, Vector2 alternativeEndPoint, Collision collision) : base(gameModeController, flag, startPosition, endPosition)
        {
            Name = "Guard";
            SpriteSheet = "1";
            _alternativeEndPoint = alternativeEndPoint;
            _collision = collision;
        }

        public override GameFrameStory Interact()
        {
            var learnedFight = GameFlags.GetVariable<bool>("learned_fight");
            var scriptName = learnedFight ? "north_guard_post_fight.ink" : "north_guard_pre_fight.ink";
            GameStory = ReadStory(scriptName);
            if(learnedFight)
            {
                GameStory.ChoosePathString("dialog");
                CompleteEvent completeEvent = victory =>
                {
                    if (victory)
                    {
                        var collision = _collision.Invoke(Position.ToPoint(), EndPosition.ToPoint());
                        var endPoint = collision ? _alternativeEndPoint : EndPosition;
                        MoveDelegate?.Invoke(this, endPoint.ToPoint());
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
