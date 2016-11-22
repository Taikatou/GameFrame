using GameFrame.Ink;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg.Entities
{
    public class VillagerEntity : NpcEntity
    {
        public enum State { Start, Monk, Mountain }

        private State _state;
        private GameFrameStory _activeStory;
        public VillagerEntity()
        {
            Name = "Villager";
            SpriteSheet = "5";
            _state = State.Start;
        }

        public override GameFrameStory Interact()
        {
            switch (_state)
            {
                case State.Start:
                    _activeStory = ReadStory("villager_start.ink");
                    break;
                case State.Monk:
                    _activeStory = ReadStory("villager_monk.ink");
                    break;
                case State.Mountain:
                    _activeStory = ReadStory("villager_mountain.ink");
                    break;
            }
            return _activeStory;
        }

        public override void CompleteInteract()
        {
            switch (_state)
            {
                case State.Start:
                    var attendTour = _activeStory.GetVariableState<int>("attend_tour") == 1;
                    if (attendTour)
                    {
                        _state = State.Monk;
                        MoveDelegate?.Invoke(this, new Point(5, 4));
                    }
                    break;
                case State.Monk:
                    _state = State.Mountain;
                    MoveDelegate?.Invoke(this, new Point(32, 8));
                    break;
                case State.Mountain:
                    _state = State.Start;
                    MoveDelegate?.Invoke(this, new Point(9, 22));
                    break;
            }
        }
    }
}
