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
            toReturn.BindFunction("battle", (string scriptName) => {
                _gameModeController.PushGameModeDelegate(new BattleGameMode(this, scriptName, _gameModeController));
            });
            return toReturn;
        }
    }
}
