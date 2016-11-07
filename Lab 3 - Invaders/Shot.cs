using System.Drawing;

namespace SpaceInvaders
{
    internal class Shot
    {
        private const int moveInterval = 15;
        private const int width = 3;
        private const int height = 10;
        private Rectangle boundaries;

        private readonly Direction direction;

        public Shot(Point location, Direction direction,
            Rectangle boundaries)
        {
            Location = location;
            this.direction = direction;
            this.boundaries = boundaries;
        }

        public Point Location { get; private set; }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Red,
                Location.X, Location.Y, width, height);
        }

        public bool Move()
        {
            Point newLocation;
            if (direction == Direction.Down)
                newLocation = new Point(Location.X, Location.Y + moveInterval);
            else //if (direction == Direction.Up)
                newLocation = new Point(Location.X, Location.Y - moveInterval);
            if ((newLocation.Y < boundaries.Height) && (newLocation.Y > 0))
            {
                Location = newLocation;
                return true;
            }
            return false;
        }
    }
}