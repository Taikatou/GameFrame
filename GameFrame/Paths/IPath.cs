using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Paths
{
    public interface IPath
    {
        Point NextPosition { get; }
        bool ToMove { get; }
        void Update(Point currentLocation);
    }
}
