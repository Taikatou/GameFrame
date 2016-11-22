using GameFrame.Ink;

namespace Demos.TopDownRpg.Entities
{
    public class MovingEntity : NpcEntity
    {
        public GameFrameStory ReadMovementStory(string storyName, Entity entity=null)
        {
            if (entity == null)
            {
                entity = this;
            }
            var endPosition = Position.ToPoint();
            var story = ReadStory(storyName);
            story.ObserveVariable("x_pos", (varName, newValue) =>
            {
                endPosition.X = (int)newValue;
                MoveDelegate?.Invoke(entity, endPosition);
            });
            story.ObserveVariable("y_pos", (varName, newValue) =>
            {
                endPosition.Y = (int)newValue;
                MoveDelegate?.Invoke(entity, endPosition);
            });
            return story;
        }
    }
}
