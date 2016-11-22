using GameFrame.Ink;

namespace Demos.TopDownRpg.Entities
{
    public class MovingEntity : NpcEntity
    {
        public GameFrameStory ReadMovementStory(string storyName)
        {
            var endPosition = Position.ToPoint();
            var story = ReadStory(storyName);
            story.ObserveVariable("x_pos", (varName, newValue) =>
            {
                endPosition.X = (int)newValue;
                MoveDelegate?.Invoke(this, endPosition);
            });
            story.ObserveVariable("y_pos", (varName, newValue) =>
            {
                endPosition.Y = (int)newValue;
                MoveDelegate?.Invoke(this, endPosition);
            });
            return story;
        }
    }
}
