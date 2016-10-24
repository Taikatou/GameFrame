using Microsoft.Xna.Framework;

namespace GameFrame.CollisionSystems.SpatialHash
{
    public interface ISpatialCollisionSystem<T> : ICollisionSystem
    {
        void AddNode(Point position, T node);
        void RemoveNode(Point point);
        T ValueAt(Point position);
    }
}
