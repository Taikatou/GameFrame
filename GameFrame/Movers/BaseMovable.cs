using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public class BaseMovable : IMovable, ISpeed
    {
        public bool Moving { get; set; }
        public Vector2 FacingDirection { get; set; }
        public Vector2 MovingDirection { get; set; }
        public Vector2 Position { get; set; }
        public virtual float Speed { get; set; }
        public EventHandler OnMoveEvent { get; set; }
        public EventHandler OnMoveCompleteEvent { get; set; }

        public void InvokeOnMoveCompleteEvent()
        {
            if (OnMoveCompleteEvent != null)
            {
                OnMoveCompleteEvent.Invoke(this, null);
                OnMoveCompleteEvent = null;
            }
        }

        public BaseMovable()
        {
            MovingDirection = new Vector2();
            FacingDirection = new Vector2();
        }
    }
}
