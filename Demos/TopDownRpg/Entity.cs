using Demos.TopDownRpg.SpeedState;
using GameFrame.Movers;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg
{
    public class Entity : BaseMovable
    {
        public SpeedContext SpeedContext;
        public override float Speed => SpeedContext.GetSpeed(2);
        //william state pattern
        public Entity(Vector2 position)
        {
            SpeedContext = new SpeedContext(new SpeedNormal());
            Position = position;
        }
    }
}
