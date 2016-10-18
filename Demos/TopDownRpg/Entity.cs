using GameFrame.Movers;
using GameFrame.SpeedState;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg
{
    public class Entity : BaseMovable
    {
        //william state pattern
        public Entity(Vector2 position)
        {
            Position = position;
            Speed = new Speed(new SpeedGrass());
            MovingDirection = new Vector2();
        }
    }
}
