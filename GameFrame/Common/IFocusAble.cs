using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Common
{
    public interface IFocusAble : IMovable
    {
        Point ScreenPosition { get; }
        Point Offset { get; }
    }
}
