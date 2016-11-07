using Microsoft.Xna.Framework;

namespace GameFrame.Movers
{
    public interface IMoverManager <in T>
    {
        bool RequestMovement(T character, Vector2 position);
    }
}
