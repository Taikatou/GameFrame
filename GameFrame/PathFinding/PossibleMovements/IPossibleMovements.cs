using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.PossibleMovements
{
    public interface IPossibleMovements
    {
        Point[] GetAdjacentLocations(Point fromLocation);
    }
}
