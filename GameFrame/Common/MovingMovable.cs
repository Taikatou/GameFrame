using System;
using GameFrame.Movers;
using Microsoft.Xna.Framework;

namespace GameFrame.Common
{
    public class MovingMovable : ICompleteAble
    {
        public bool Complete => Time <= 0;
        public float Time { get; internal set; }
        public float TotalTime { get; }
        public EventHandler OnCompleteEvent { get; set; }
        public float Progress => Complete ? 100.0f : Time / TotalTime;
        private readonly BaseMovable _moving;
        private Vector2 _firstPosition;

        public void InvokeCompleteEvent()
        {
            OnCompleteEvent?.Invoke(this, null);
        }

        public MovingMovable(BaseMovable moving, float time)
        {
            _moving = moving;
            _firstPosition = new Vector2(_moving.Position.X, _moving.Position.Y);
            Time = 0;
            TotalTime = time;
        }

        public void Update(GameTime time)
        {
            if (!Complete)
            {
                Time += time.ElapsedGameTime.Milliseconds;
                _moving.Position = _firstPosition + _moving.MovingDirection*Progress;
            }
        }
    }
}
