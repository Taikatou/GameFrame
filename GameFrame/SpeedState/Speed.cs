namespace GameFrame.SpeedState
{
    public class Speed : ISpeed
    {
        public ISpeedState State { get; private set; }

        public Speed(ISpeedState currentSpeedState)
        {
            State = currentSpeedState;
        }

        public void ToGrass()
        {
            State = new SpeedGrass();
        }

        public void ToMud()
        {
            State = new SpeedMud();
        }

        public void ToWater()
        {
            State = new SpeedWater();
        }

        public void SetState(ISpeedState speedState)
        {
            State = speedState;
        }
    }
}
