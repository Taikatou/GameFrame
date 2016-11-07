using System;
using System.Drawing;
using SpaceInvaders.Properties;

namespace SpaceInvaders
{
    internal class PlayerShip
    {
        private const int horizontalInterval = 10;

        private bool alive;

        private Rectangle boundaries;

        private float deadShipHeight;

        private DateTime deathWait;
        public Bitmap image = Resources.player;

        public PlayerShip(Rectangle boundaries, Point location)
        {
            this.boundaries = boundaries;
            Location = location;
            Alive = true;
            deadShipHeight = 1.0F;
        }

        public Point Location { get; private set; }

        public Rectangle Area
        {
            get { return new Rectangle(Location, image.Size); }
        }

        public bool Alive
        {
            get { return alive; }
            set
            {
                alive = value;
                if (!value)
                    deathWait = DateTime.Now;
            }
        }

        public void Move(Direction direction)
        {
            if (Alive)
                if (direction == Direction.Left)
                {
                    var newLocation = new Point(Location.X - horizontalInterval, Location.Y);
                    if ((newLocation.X < boundaries.Width - 100) && (newLocation.X > 50))
                        Location = newLocation;
                }
                else if (direction == Direction.Right)
                {
                    var newLocation = new Point(Location.X + horizontalInterval, Location.Y);
                    if ((newLocation.X < boundaries.Width - 100) && (newLocation.X > 50))
                        Location = newLocation;
                }
        }

        public void Draw(Graphics graphics)
        {
            if (!Alive)
            {
                if (DateTime.Now - deathWait > TimeSpan.FromSeconds(1.5))
                {
                    deadShipHeight = 0.0F;
                    Alive = true;
                }
                else if (DateTime.Now - deathWait > TimeSpan.FromSeconds(1))
                {
                    deadShipHeight = 0.25F;
                }
                else if (DateTime.Now - deathWait > TimeSpan.FromSeconds(0.5))
                {
                    deadShipHeight = 0.75F;
                }
                else if (DateTime.Now - deathWait > TimeSpan.FromSeconds(0))
                {
                    deadShipHeight = 0.9F;
                }

                graphics.DrawImage(image, Location.X, Location.Y,
                    image.Width, image.Height*deadShipHeight);
            }
            else
            {
                graphics.DrawImage(image, Location);
            }
        }
    }
}