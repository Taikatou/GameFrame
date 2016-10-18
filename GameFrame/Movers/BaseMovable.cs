using System;
using System.Diagnostics;
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

        private int _counter = 0;


        public void FaceMovingDirection()
        {
            FacingDirection = MovingDirection;
        }

        internal int GetSpeed()
        {
            if (_counter < 500)
            {
                _counter++;
                return Speed.State.Speed;
            }
            if (_counter == 500)
            {
                _counter++;
                Debug.WriteLine("CHANGE STATE");
            }
            Speed.SetState(new SpeedMud());
            return Speed.State.Speed;
        }
    }
}
