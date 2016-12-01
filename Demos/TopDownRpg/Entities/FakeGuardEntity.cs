using GameFrame.Ink;

namespace Demos.TopDownRpg.Entities
{
    public class FakeGuardEntity : NpcEntity
    {
        public FakeGuardEntity()
        {
            Name = "Guard";
            SpriteSheet = "5";
        }

        public override GameFrameStory Interact()
        {
            return ReadStory("fake_guard.ink");
        }
    }
}
