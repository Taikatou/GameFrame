namespace Demos.TopDownRpg
{
    public class Flags
    {
        public static int FishCount
        {
            get { return GameFlags.GetVariable<int>("fish_count"); }
            set { GameFlags.SetVariable("fish_count", value); }
        }

        public static bool PrincessKidnapped
        {
            get { return GameFlags.GetVariable<bool>("princess_kidnapped"); }
            set { GameFlags.SetVariable("princess_kidnapped", value); }
        }
    }
}
