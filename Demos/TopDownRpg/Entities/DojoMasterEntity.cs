using GameFrame.Ink;

namespace Demos.TopDownRpg.Entities
{
    public class DojoMasterEntity : AbstractCompleteEntity
    {
        public override GameFrameStory StoryScript => ReadMovementStory("dojo_master.ink");
        public override GameFrameStory CompleteScript => ReadStory("dojo_master_complete.ink");
        public DojoMasterEntity()
        {
            Name = "Dojo Master";
            SpriteSheet = "1";
        }
    }
}
