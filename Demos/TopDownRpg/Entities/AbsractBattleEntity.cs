using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public abstract class AbsractBattleEntity : SwitchNpcEntity
    {
        public abstract string BattleScriptName { get; }
        private readonly GameModeController _gameModeController;

        protected AbsractBattleEntity(GameModeController gameModeController, string flag, Vector2 startPosition, Vector2 endPosition) : base(flag, startPosition, endPosition)
        {
            _gameModeController = gameModeController;
        }

        public override GameFrameStory ReadStory(string story)
        {
            var toReturn = base.ReadStory(story);
            toReturn.BindFunction("battle", (string scriptName) =>
            {
                var battleMode = new BattleGameMode(this)
                {
                    CompleteEvent = (sender, args) =>
                    {
                        _gameModeController.PopGameMode();
                    }
                };
                battleMode.StartStory(scriptName);
                _gameModeController.PushGameModeDelegate(battleMode);
            });
            return toReturn;
        }
    }
}
