using Microsoft.Xna.Framework;

namespace GameFrame.Movers
{
    public interface IMoverManager <T>
    {
        bool RequestMovement(T character, Vector2 position);
    }
}
