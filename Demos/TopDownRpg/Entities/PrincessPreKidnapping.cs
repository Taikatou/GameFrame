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
            GameStory = ReadMovementStory("princess_pre_kidnapping.ink", _fakeGuard);
            GameStory.ObserveVariable("fwacked", (varName, newValue) =>
            {
                if (!Fwacked)
                {
                    Fwacked = true;
                    _removeEntity.Invoke(_fakeGuard);
                    _removeEntity.Invoke(this);
                    GameFlags.AddObject("princess_kidnapped", true);
                }
            });
            return GameStory;
        }
    }
}
