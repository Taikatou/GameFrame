using System;
using Microsoft.Xna.Framework;

namespace GameFrame.Common
{
    public class ExpiringKey
    {
        public bool Complete => TimeLeft <= 0;
        public float TimeLeft { get; internal set; }
        public float TotalTime { get; }
        public EventHandler OnCompleteEvent { get; set; }
        public float Progress => Complete ? 0.0f : TimeLeft / TotalTime;

        public void InvokeCompleteEvent()
        {
            OnCompleteEvent?.Invoke(this, null);
        }

        public ExpiringKey(float time)
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
