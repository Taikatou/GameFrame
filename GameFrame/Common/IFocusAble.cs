using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Common
{
    public interface IFocusAble : IMovable
    {
        Vector2 ScreenPosition { get; }
        Vector2 Offset { get; }
    }
}
