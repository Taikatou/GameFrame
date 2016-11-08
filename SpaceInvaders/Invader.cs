using System;
using System.Drawing;
using System.Windows.Forms;
using SpaceInvaders.Properties;

namespace SpaceInvaders
{
    internal class Invader
    {
        private const int HorizontalInterval = 10;
        private const int VerticalInterval = 30;

        private Bitmap _image;
        private Bitmap[] _imageArray;

        public Invader(ShipType invaderType, Point location, int score)
        {
            InvaderType = invaderType;
            Location = location;
            Score = score;

            CreateInvaderBitmapArray();
            _image = _imageArray[0];
        }

        public Point Location { get; private set; }

        public ShipType InvaderType { get; }

        public Rectangle Area
        {
            get { return new Rectangle(Location, _imageArray[0].Size); }
        }

        private int Score { get; set; }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    // Location is a struct, so new one created to keep it immutable
                    Location = new Point(Location.X + HorizontalInterval, Location.Y);
                    break;
                case Direction.Left:
                    Location = new Point(Location.X - HorizontalInterval, Location.Y);
                    break;
                case Direction.Down:
                    Location = new Point(Location.X, Location.Y + VerticalInterval);
                    break;
            }
        }

        public Graphics Draw(Graphics graphics, int animationCell)
        {
            var invaderGraphics = graphics;
            _image = _imageArray[animationCell];
            try
            {
                graphics.DrawImage(_image, Location);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            //DEBUG red square invaders
            //graphics.FillRectangle(Brushes.Red,
            //    Location.X, Location.Y, 20, 20);
            return invaderGraphics;
        }

        private void CreateInvaderBitmapArray()
        {
            _imageArray = new Bitmap[4];
            switch (InvaderType)
            {
                case ShipType.Bug:
                    _imageArray[0] = Resources.bug1;
                    _imageArray[1] = Resources.bug2;
                    _imageArray[2] = Resources.bug3;
                    _imageArray[3] = Resources.bug4;
                    break;
                case ShipType.Satellite:
                    _imageArray[0] = Resources.satellite1;
                    _imageArray[1] = Resources.satellite2;
                    _imageArray[2] = Resources.satellite3;
                    _imageArray[3] = Resources.satellite4;
                    break;
                case ShipType.Saucer:
                    _imageArray[0] = Resources.flyingsaucer1;
                    _imageArray[1] = Resources.flyingsaucer2;
                    _imageArray[2] = Resources.flyingsaucer3;
                    _imageArray[3] = Resources.flyingsaucer4;
                    break;
                case ShipType.Spaceship:
                    _imageArray[0] = Resources.spaceship1;
                    _imageArray[1] = Resources.spaceship2;
                    _imageArray[2] = Resources.spaceship3;
                    _imageArray[3] = Resources.spaceship4;
                    break;
                case ShipType.Star:
                    _imageArray[0] = Resources.star1;
                    _imageArray[1] = Resources.star2;
                    _imageArray[2] = Resources.star3;
                    _imageArray[3] = Resources.star4;
                    break;
            }
        }
    }
}