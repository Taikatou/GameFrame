using GameFrame.CollisionSystems.SpatialHash;

namespace GameFrame.Movers
{
    public class ExpiringSpatialHashMovementComplete <T> : ICompleteAble where T : BaseMovable
    {
        private readonly ExpiringSpatialHashCollisionSystem<T> _expiringSpatialHash;
        private readonly T _moving;
        public ExpiringSpatialHashMovementComplete(ExpiringSpatialHashCollisionSystem<T> expiringSpatialHash, T moving)
        {
            _moving = moving;
            _expiringSpatialHash = expiringSpatialHash;
        }

        public bool Complete
        {
            get
            {
                var oldPosition = _expiringSpatialHash.Moving(_moving.Position.ToPoint());
                return oldPosition;
            }
        }
    }
}
