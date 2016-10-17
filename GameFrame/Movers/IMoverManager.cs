using Microsoft.Xna.Framework;

namespace GameFrame.Movers
{
    public interface IMoverManager
    {
        bool RequestMovement(Vector2 position);
    }
}
