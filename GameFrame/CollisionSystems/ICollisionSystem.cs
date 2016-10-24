using Microsoft.Xna.Framework;

namespace GameFrame.CollisionSystems
{
    public interface ICollisionSystem
    {
        bool CheckCollision(Point p);
    }
}
