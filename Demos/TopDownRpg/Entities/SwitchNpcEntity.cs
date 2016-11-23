using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class SwitchNpcEntity : NpcEntity
    {
        public bool AlreadyMoved;
        public SwitchNpcEntity(string flag, Vector2 startPosition, Vector2 endPosition)
        {
            AlreadyMoved = GameFlags.GetFlag<bool>(flag);
            Position = AlreadyMoved ? endPosition : startPosition;
        }
    }
}
