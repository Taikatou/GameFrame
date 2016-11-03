using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public abstract class AbstractMovable : IMovable, ISpeed
    {
        public bool Moving { get; set; }
        public Vector2 FacingDirection { get; set; }
        public Vector2 MovingDirection { get; set; }
        public Vector2 Position { get; set; }
        public abstract float Speed { get; }
        public EventHandler OnMoveCompleteEvent { get; set; }

        protected AbstractMovable()
        {
            MovingDirection = new Vector2();
            FacingDirection = new Vector2();
        }
    }
}
