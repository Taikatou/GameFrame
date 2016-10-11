using Microsoft.Xna.Framework;

namespace GameFrame.Movers
{
    public interface IMover
    {
        void RequestMovement(Vector2 position);
    }
}
