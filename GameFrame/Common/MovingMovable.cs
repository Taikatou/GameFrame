using System;
using GameFrame.Movers;
using Microsoft.Xna.Framework;

namespace GameFrame.Common
{
    public class MovingMovable : ICompleteAble
    {
        public bool Complete => TimeLeft <= 0;
        public float TimeLeft { get; internal set; }
        public float TotalTime { get; }
        public EventHandler OnCompleteEvent { get; set; }
        public float Progress => Complete ? 0.0f : TimeLeft / TotalTime;
        private BaseMovable _moving;
        private Vector2 _firstPosition;

        public void InvokeCompleteEvent()
        {
            OnCompleteEvent?.Invoke(this, null);
        }

        public MovingMovable(BaseMovable moving, float time)
        {
            _moving = moving;
            _firstPosition = new Vector2(_moving.Position.X, _moving.Position.Y);
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
