using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public abstract class AbsractBattleEntity : SwitchNpcEntity
    {
        private readonly GameModeController _gameModeController;
        public CompleteEvent CompleteEvent;
        public GameFrameStory GameStory;
        public readonly string FlagName;

        protected AbsractBattleEntity(GameModeController gameModeController, string flag, Vector2 startPosition, Vector2 endPosition) : base(flag, startPosition, endPosition)
        {
            FlagName = flag;
            _gameModeController = gameModeController;
        }

        public GameFrameStory ReadStory(GameFrameStory story, CompleteEvent completeEvent)
        {
            story.BindFunction("battle", (string scriptName) =>
            {
                var battleMode = new BattleGameMode(this, TopDownRpgScene.ClickEvent)
                {
                    CompleteEvent = victory =>
                    {
                        _gameModeController.PopGameMode();
                        completeEvent?.Invoke(victory);
                    }
                };
                battleMode.StartStory(scriptName);
                _gameModeController.PushGameModeDelegate(battleMode);
            });
            return story;
        }
    }
}
