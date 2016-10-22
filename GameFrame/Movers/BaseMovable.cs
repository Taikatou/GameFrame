using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public class BaseMovable : IMovable, ISpeed
    {
        public bool Moving { get; set; }
        public Vector2 FacingDirection { get; internal set; }
        public Vector2 MovingDirection { get; set; }
        public Vector2 Position { get; set; }
        public virtual float Speed { get; }

        public void FaceMovingDirection()
        {
            FacingDirection = MovingDirection;
        }
    }
}
