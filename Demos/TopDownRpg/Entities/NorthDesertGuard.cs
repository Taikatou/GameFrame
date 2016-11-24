using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class NorthDesertGuard : SwitchNpcEntity
    {
        private GameFrameStory _gameStory;
        private readonly string _flagName;

        public NorthDesertGuard(string flag, Vector2 startPosition, Vector2 endPosition) : base(flag, startPosition, endPosition)
        {
            _flagName = flag;
            Name = "Guard";
            SpriteSheet = "1";
        }

        public override GameFrameStory Interact()
        {
            var learnedFight = GameFlags.GetFlag<bool>("learned_fight");
            var scriptName = learnedFight ? "north_guard_post_fight.ink" : "north_guard_pre_fight.ink";
            _gameStory = ReadStory(scriptName);
            return _gameStory;
        }

        public override void CompleteInteract()
        {
            if (!AlreadyMoved)
            {
                var toFight = _gameStory.GetVariableState<int>("to_fight") == 1;
                if (toFight)
                {
                    MoveDelegate?.Invoke(this, new Point(24, 35));
                    GameFlags.AddObject(_flagName, true);
                    AlreadyMoved = true;
                }
            }
        }
    }
}
