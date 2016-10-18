using GameFrame.Movers;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg
{
    public class Entity : BaseMovable
    {
        //william state pattern
        public Entity(Vector2 position)
        {
            Position = position;
            Speed = 200;
            MovingDirection = new Vector2();
        }
    }
}
