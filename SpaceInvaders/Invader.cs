using System;
using System.Drawing;
using System.Windows.Forms;
using SpaceInvaders.Properties;

namespace SpaceInvaders
{
    internal class Invader
    {
        private const int horizontalInterval = 10;
        private const int verticalInterval = 30;

        private Bitmap image;
        private Bitmap[] imageArray;

        public Invader(ShipType invaderType, Point location, int score)
        {
            InvaderType = invaderType;
            Location = location;
            Score = score;

            createInvaderBitmapArray();
            image = imageArray[0];
        }

        public Point Location { get; private set; }

        public ShipType InvaderType { get; }

        public Rectangle Area
        {
            get { return new Rectangle(Location, imageArray[0].Size); }
        }

        public int Score { get; private set; }

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    // Location is a struct, so new one created to keep it immutable
                    Location = new Point(Location.X + horizontalInterval, Location.Y);
                    break;
                case Direction.Left:
                    Location = new Point(Location.X - horizontalInterval, Location.Y);
                    break;
                case Direction.Down:
                    Location = new Point(Location.X, Location.Y + verticalInterval);
                    break;
            }
        }

        public Graphics Draw(Graphics graphics, int animationCell)
        {
            var invaderGraphics = graphics;
            image = imageArray[animationCell];
            try
            {
                graphics.DrawImage(image, Location);
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

        private void createInvaderBitmapArray()
        {
            imageArray = new Bitmap[4];
            switch (InvaderType)
            {
                case ShipType.Bug:
                    imageArray[0] = Resources.bug1;
                    imageArray[1] = Resources.bug2;
                    imageArray[2] = Resources.bug3;
                    imageArray[3] = Resources.bug4;
                    break;
                case ShipType.Satellite:
                    imageArray[0] = Resources.satellite1;
                    imageArray[1] = Resources.satellite2;
                    imageArray[2] = Resources.satellite3;
                    imageArray[3] = Resources.satellite4;
                    break;
                case ShipType.Saucer:
                    imageArray[0] = Resources.flyingsaucer1;
                    imageArray[1] = Resources.flyingsaucer2;
                    imageArray[2] = Resources.flyingsaucer3;
                    imageArray[3] = Resources.flyingsaucer4;
                    break;
                case ShipType.Spaceship:
                    imageArray[0] = Resources.spaceship1;
                    imageArray[1] = Resources.spaceship2;
                    imageArray[2] = Resources.spaceship3;
                    imageArray[3] = Resources.spaceship4;
                    break;
                case ShipType.Star:
                    imageArray[0] = Resources.star1;
                    imageArray[1] = Resources.star2;
                    imageArray[2] = Resources.star3;
                    imageArray[3] = Resources.star4;
                    break;
            }
        }
    }
}