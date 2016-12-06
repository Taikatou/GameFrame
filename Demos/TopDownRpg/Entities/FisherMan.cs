using GameFrame.Ink;

namespace Demos.TopDownRpg.Entities
{
    public class FisherMan : NpcEntity
    {
        private GameFrameStory _gameFrameStory;
        public FisherMan()
        {
            Name = "Fisher man";
            SpriteSheet = "2";
        }

        public override GameFrameStory Interact()
        {
            var scriptFile = Flags.AcquireRod ? "fisher_complete.ink" : "fisher.ink";
            _gameFrameStory = ReadStory(scriptFile);
            return _gameFrameStory;
        }

        public override void CompleteInteract()
        {
            if (!Flags.AcquireRod)
            {
                var storyOver = _gameFrameStory.GetVariableState<int>("acquire_rod") == 1;
                if (storyOver)
                {
                    GameFlags.SetVariable("acquire_rod", true);
                }
            }
        }
    }
}
