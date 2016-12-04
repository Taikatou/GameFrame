using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class HideoutGuard : AbsractBattleEntity
    {
        public string ScriptName;
        private readonly Collision _collision;
        private readonly Vector2 _alternativeEndPoint;
        public HideoutGuard(string scriptName, GameModeController gameModeController, string flag, Vector2 startPosition, Vector2 endPosition, Vector2 alternativeEndPoint, Collision collision) : base(gameModeController, flag, startPosition, endPosition)
        {
            Name = "Guard";
            SpriteSheet = "1";
            ScriptName = scriptName;
            _collision = collision;
            _alternativeEndPoint = alternativeEndPoint;
        }

        public override GameFrameStory Interact()
        {
            GameStory = ReadStory(ScriptName);
            GameStory.ChoosePathString("dialog");
            CompleteEvent completeEvent = win =>
            {
                if (win)
                {
                    var collision = _collision.Invoke(Position.ToPoint(), EndPosition.ToPoint());
                    var endPoint = collision ? _alternativeEndPoint : EndPosition;
                    MoveDelegate?.Invoke(this, endPoint.ToPoint());
                    GameFlags.SetVariable(FlagName, true);
                    AlreadyMoved = true;
                }
            };
            ReadStory(GameStory, completeEvent);
            return GameStory;
        }
    }
}
