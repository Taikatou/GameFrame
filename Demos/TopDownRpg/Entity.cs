using Demos.TopDownRpg.SpeedState;
using GameFrame.Movers;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg
{
    public class Entity : BaseMovable
    {
        public SpeedContext SpeedContext;
        public override float Speed => SpeedContext.Speed;

        public Entity(Vector2 position)
        {
            SpeedContext = new SpeedContext(4 * 16);
            Position = position;
        }

        public virtual void Interact()
        {
            
        }
    }
}
