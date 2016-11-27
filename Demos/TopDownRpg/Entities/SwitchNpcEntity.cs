using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class SwitchNpcEntity : NpcEntity
    {
        public bool AlreadyMoved;
        public Vector2 EndPosition;
        public SwitchNpcEntity(string flag, Vector2 startPosition, Vector2 endPosition)
        {
            AlreadyMoved = GameFlags.GetVariable<bool>(flag);
            Position = AlreadyMoved ? endPosition : startPosition;
            EndPosition = endPosition;
        }
    }
}
