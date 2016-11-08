using System;
using System.Drawing;
using SpaceInvaders.Properties;

namespace SpaceInvaders
{
    internal class PlayerShip
    {
        private const int HorizontalInterval = 10;
        public readonly Bitmap Image = Resources.player;

        private bool _alive;

        private Rectangle _boundaries;

        private float _deadShipHeight;

        private DateTime _deathWait;

        public PlayerShip(Rectangle boundaries, Point location)
        {
            _boundaries = boundaries;
            Location = location;
            Alive = true;
            _deadShipHeight = 1.0F;
        }

        public Point Location { get; private set; }

        public Rectangle Area
        {
            get { return new Rectangle(Location, Image.Size); }
        }

        public bool Alive
        {
            get { return _alive; }
            set
            {
                _alive = value;
                if (!value)
                    _deathWait = DateTime.Now;
            }
        }

        public void Move(Direction direction)
        {
            if (Alive)
                if (direction == Direction.Left)
                {
                    var newLocation = new Point(Location.X - HorizontalInterval, Location.Y);
                    if ((newLocation.X < _boundaries.Width - 100) && (newLocation.X > 50))
                        Location = newLocation;
                }
                else if (direction == Direction.Right)
                {
                    var newLocation = new Point(Location.X + HorizontalInterval, Location.Y);
                    if ((newLocation.X < _boundaries.Width - 100) && (newLocation.X > 50))
                        Location = newLocation;
                }
        }

        public void Draw(Graphics graphics)
        {
            if (!Alive)
            {
                if (DateTime.Now - _deathWait > TimeSpan.FromSeconds(1.5))
                {
                    _deadShipHeight = 0.0F;
                    Alive = true;
                }
                else if (DateTime.Now - _deathWait > TimeSpan.FromSeconds(1))
                {
                    _deadShipHeight = 0.25F;
                }
                else if (DateTime.Now - _deathWait > TimeSpan.FromSeconds(0.5))
                {
                    _deadShipHeight = 0.75F;
                }
                else if (DateTime.Now - _deathWait > TimeSpan.FromSeconds(0))
                {
                    _deadShipHeight = 0.9F;
                }

                graphics.DrawImage(Image, Location.X, Location.Y,
                    Image.Width, Image.Height*_deadShipHeight);
            }
            else
            {
                graphics.DrawImage(Image, Location);
            }
        }
    }
}