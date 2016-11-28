using GameFrame.Ink;

namespace Demos.TopDownRpg.Entities
{
    public class PrincessKidnapped : Princess
    {
        public override GameFrameStory Interact()
        {
            return ReadStory("princess_kidnapped.ink");
        }
    }
}
