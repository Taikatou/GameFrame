using Microsoft.Xna.Framework;

namespace GameFrame.Common
{
    public class ExpiringKey
    {
        public bool Complete => TimeLeft <= 0;
        public int TimeLeft { get; internal set; }
        public int TotalTime { get; }

        public float Progress => Complete ? 0.0f : (float)TimeLeft / TotalTime;

        public ExpiringKey(int time)
        {
            TimeLeft = time;
            TotalTime = time;
        }

        public void Update(GameTime time)
        {
            if (!Complete)
            {
                TimeLeft -= time.ElapsedGameTime.Milliseconds;
            }
        }
    }
}
