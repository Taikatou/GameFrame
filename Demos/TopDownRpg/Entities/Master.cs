using GameFrame.Ink;

namespace Demos.TopDownRpg.Entities
{
    public class Master : AbstractCompleteEntity
    {
        public override GameFrameStory StoryScript => ReadStory("master_pre_learn_fight.ink");
        public override GameFrameStory CompleteScript => ReadStory("master_complete.ink");
        public string GaveFishVariable => "give_fish";
        public bool PrincessKidnapped => GameFlags.GetVariable<bool>("princess_kidnapped");
        public bool GaveFish;


        public Master()
        {
            Name = "Master";
            SpriteSheet = "5";
            GaveFish = GameFlags.GetVariable<bool>(GaveFishVariable);
        }

        public override GameFrameStory Interact()
        {
            if (PrincessKidnapped)
            {
                if (GaveFish)
                {
                    GameStory = base.Interact();
                }
                else
                {
                    GameStory = ReadStory("master_pre_fishes.ink");
                    var fishCount = GameFlags.GetVariable(Global.FishCountVariable, 0);
                    GameStory.ChoosePathString("dialog");
                    GameStory.SetVariableState(Global.FishCountVariable, fishCount);
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
            if (PrincessKidnapped)
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
                        var fishCount = GameFlags.GetVariable<int>(Global.FishCountVariable);
                        GameFlags.SetVariable(Global.FishCountVariable, fishCount - 3);
                    }
                }
            }
        }
    }
}
