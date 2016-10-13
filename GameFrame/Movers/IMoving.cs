using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Movers
{
    public interface IMoving : IMovable
    {
        bool Moving { get; set; }
        Vector2 Direction { get; set; }
    }
}
