using GameFrame.Movers;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg
{
    public class Entity : BaseMovable
    {
        public Entity(Vector2 position)
        {
            Position = position;
            MovingDirection = new Vector2();
        }
    }
}
