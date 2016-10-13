using Microsoft.Xna.Framework;

namespace GameFrame.Movers
{
    public interface IMoverManager
    {
        void RequestMovement(Vector2 position);
    }
}
