using Demos.TopDownRpg.GameModes;
using GameFrame.Ink;

namespace Demos.TopDownRpg.Entities
{
    public class PrincessPreKidnapping : Princess
    {
        private readonly FakeGuardEntity _fakeGuard;
        public GameFrameStory GameStory;
        public bool Fwacked;
        private readonly RemoveEntity _removeEntity;

        public PrincessPreKidnapping(FakeGuardEntity fakeGuard, RemoveEntity removeEntity)
        {
            _fakeGuard = fakeGuard;
            _removeEntity = removeEntity;
        }

        public override GameFrameStory Interact()
        {
            GameStory = ReadStory("princess_pre_kidnapping.ink");
            GameStory.ObserveVariable("move_bandit", (varName, newValue) =>
            {
                var moveTo = PlayerEntity.Instance.Position.ToPoint();
                MoveDelegate(_fakeGuard, moveTo);
            });
            GameStory.ObserveVariable("fwacked", (varName, newValue) =>
            {
                if (!Fwacked)
                {
                    Fwacked = true;
                    _removeEntity.Invoke(_fakeGuard);
                    _removeEntity.Invoke(this);
                    GameFlags.SetVariable("princess_kidnapped", true);
                }
            });
            return GameStory;
        }
    }
}
