using GameFrame.Ink;

namespace Demos.TopDownRpg.Entities
{
    public class Master : AbstractCompleteEntity
    {
        public override GameFrameStory StoryScript => ReadStory("master_pre_learn_fight.ink");
        public override GameFrameStory CompleteScript => ReadStory("master_complete.ink");
        public string GaveFishVariable => "give_fish";
        public bool GaveFish;


        public Master()
        {
            Name = "Master";
            SpriteSheet = "5";
            GaveFish = GameFlags.GetVariable<bool>(GaveFishVariable);
        }

        public override GameFrameStory Interact()
        {
            if (Flags.PrincessKidnapped)
            {
                if (GaveFish)
                {
                    GameStory = base.Interact();
                }
                else
                {
                    GameStory = ReadStory("master_pre_fishes.ink");
                    var fishCount = Flags.FishCount;
                    GameStory.ChoosePathString("dialog");
                    GameStory.SetVariableState("fish_count", fishCount);
                }
            }
            else
            {
                GameStory = ReadStory("master_pre_kidnapping.ink");
            }
            return GameStory;
        }

        public override void CompleteInteract()
        {
            if (Flags.PrincessKidnapped)
            {
                if (GaveFish)
                {
                    var preStoryOver = StoryOver;
                    base.CompleteInteract();
                    if (StoryOver && !preStoryOver)
                    {
                        GameFlags.SetVariable("learned_fight", true);
                    }
                }
                else
                {
                    GaveFish = GameStory.GetVariableState<int>(GaveFishVariable) == 1;
                    if (GaveFish)
                    {
                        GameFlags.SetVariable(GaveFishVariable, GaveFish);
                        Flags.FishCount -= 3;
                    }
                }
            }
        }
    }
}
