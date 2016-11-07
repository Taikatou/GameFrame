namespace GameFrame.Paths.Reversing
{
    public class ReversingCounter
    {
        private int _direction;
        public int CurrentIndex;
        public readonly int EndIndex;

        public ReversingCounter(int startIndex, int endIndex, int direction=1)
        {
            CurrentIndex = startIndex;
            EndIndex = endIndex;
            _direction = direction;
        }

        public void Increment()
        {
            CurrentIndex += _direction;
            if(CurrentIndex == EndIndex-1 || CurrentIndex == 0)
            {
                _direction = -(_direction);
            }
        }
    }
}
