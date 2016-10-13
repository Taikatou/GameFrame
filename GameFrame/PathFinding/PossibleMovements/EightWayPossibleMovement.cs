using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding.PossibleMovements
{
    public class EightWayPossibleMovement : IPossibleMovements
    {
        public Point[] GetAdjacentLocations(Point fromLocation)
        {
            return new[]
            {
                //four way movent
                new Point(fromLocation.X-1, fromLocation.Y  ),
                new Point(fromLocation.X,   fromLocation.Y+1),
                new Point(fromLocation.X+1, fromLocation.Y  ),
                new Point(fromLocation.X,   fromLocation.Y-1),

                //diagonally
                new Point(fromLocation.X-1, fromLocation.Y-1),
                new Point(fromLocation.X+1, fromLocation.Y-1),
                new Point(fromLocation.X+1, fromLocation.Y+1),
                new Point(fromLocation.X-1, fromLocation.Y+1)
            };
        }
    }
}
