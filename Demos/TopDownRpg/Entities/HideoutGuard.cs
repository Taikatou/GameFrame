using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class HideoutGuard : AbsractBattleEntity
    {
        private GameFrameStory _gameStory;
        private readonly string _flagName;
        public HideoutGuard(GameModeController gameModeController, string flag, Vector2 startPosition, Vector2 endPosition) : base(gameModeController, flag, startPosition, endPosition)
        {
            _flagName = flag;
            Name = "Guard";
            SpriteSheet = "1";
        }

        public override GameFrameStory Interact()
        {
            _gameStory = ReadStory("hideout_guard.ink");
            _gameStory.ChoosePathString("dialog");
            CompleteEvent completeEvent = win =>
            {
                _gameStory.SetVariableState("to_move", true);
            };
            ReadStory(_gameStory, completeEvent);
            return _gameStory;
        }

        public override void CompleteInteract()
        {
            if (!AlreadyMoved)
            {
                var toMove = _gameStory.GetVariableState<int>("to_move") == 1;
                if (toMove)
                {
                    MoveDelegate?.Invoke(this, new Point(24, 35));
                    GameFlags.SetVariable(_flagName, true);
                    AlreadyMoved = true;
                }
            }
        }
    }
}
