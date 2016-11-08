using System.Drawing;

namespace SpaceInvaders
{
    internal class Shot
    {
        private const int MoveInterval = 15;
        private const int Width = 3;
        private const int Height = 10;

        private readonly Direction _direction;
        private Rectangle _boundaries;

        public Shot(Point location, Direction direction,
            Rectangle boundaries)
        {
            Location = location;
            _direction = direction;
            _boundaries = boundaries;
        }

        public Point Location { get; private set; }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Red,
                Location.X, Location.Y, Width, Height);
        }

        public bool Move()
        {
            Point newLocation;
            if (_direction == Direction.Down)
                newLocation = new Point(Location.X, Location.Y + MoveInterval);
            else //if (direction == Direction.Up)
                newLocation = new Point(Location.X, Location.Y - MoveInterval);
            if ((newLocation.Y < _boundaries.Height) && (newLocation.Y > 0))
            {
                Location = newLocation;
                return true;
            }
            return false;
        }
    }
}