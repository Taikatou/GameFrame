using GameFrame.Movers;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg
{
    public class Entity : IMover
    {
        public Vector2 Position { get; set; }
        public bool Moving { get; set; }
        public Vector2 Direction { get; set; }

        public Entity(Vector2 position)
        {
            Position = position;
            Direction = new Vector2();
        }
    }
}
