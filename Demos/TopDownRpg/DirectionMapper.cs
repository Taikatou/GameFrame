using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg
{
    public delegate Rectangle CalculateRectangle(Point tileSize);
    public class DirectionMapper
    {
        private static DirectionMapper _instance;
        public static DirectionMapper Instance => _instance ?? (_instance = new DirectionMapper());
        public Dictionary<Point, CalculateRectangle> Directions;
        public static CalculateRectangle Down = size => new Rectangle(new Point(), size);
        public static CalculateRectangle Up = size => new Rectangle(new Point(size.X, 0), size);
        public static CalculateRectangle Right = size => new Rectangle(new Point(size.X * 3, 0), size);
        public static CalculateRectangle Left = size => new Rectangle(new Point(size.X * 2, 0), size);
        protected DirectionMapper()
        {
            Directions = new Dictionary<Point, CalculateRectangle>
            {
                [new Point(0, 1)] = Down,
                [new Point(1, 0)] = Right,
                [new Point(1, 1)] = Right,
                [new Point(1, -1)] = Right,
                [new Point(0, -1)] = Up,
                [new Point(-1, 0)] = Left,
                [new Point(-1, -1)] = Left,
                [new Point(-1, 1)] = Left
            };
        }

        public static Rectangle GetRectangle(Point tileSize, Point direction)
        {
            var directions = Instance.Directions;
            if (directions.ContainsKey(direction))
            {
                return directions[direction].Invoke(tileSize);
            }
            else
            {
                return new Rectangle(new Point(), tileSize);
            }
        }
    }
}
