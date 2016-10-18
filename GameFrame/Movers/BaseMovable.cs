using System;
using GameFrame.SpeedState;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public class BaseMovable : IMovable
    {
        public bool Moving { get; set; }
        public Vector2 FacingDirection { get; internal set; }
        public Vector2 MovingDirection { get; set; }
        public Vector2 Position { get; set; }
        public ISpeed Speed { get; set; }

        public void FaceMovingDirection()
        {
            FacingDirection = MovingDirection;
        }

        internal int GetSpeed()
        {
            return Speed.State.Speed;
        }
    }
}
