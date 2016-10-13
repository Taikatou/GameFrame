using GameFrame.CollisionSystems.SpatialHash;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public abstract class AbstractMover <T>
    {
        public abstract IMovable ToMove { get; }
        public ExpiringSpatialHashCollisionSystem<T> SpatialHashLayer;

        protected AbstractMover(ExpiringSpatialHashCollisionSystem<T> spatialHashLayer)
        {
            SpatialHashLayer = spatialHashLayer;
        }
        public bool MoveAbleMoving
        {
            get
            {
                var position = ToMove.Position.ToPoint();
                var entityMoving = SpatialHashLayer.Moving(position);
                return entityMoving;
            }
        }
    }
}
