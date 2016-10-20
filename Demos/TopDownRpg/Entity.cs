using Demos.TopDownRpg.SpeedState;
using GameFrame.Movers;
using GameFrame.State;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg
{
    public class Entity : BaseMovable
    {
        public IState<int> SpeedState;
        public override int Speed => SpeedState.Modifier;
        //william state pattern
        public Entity(Vector2 position)
        {
            SpeedState = new SpeedNormal<int>();
            Position = position;
            MovingDirection = new Vector2();
        }
    }
}
