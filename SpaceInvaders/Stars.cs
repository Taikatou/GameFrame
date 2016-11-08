using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders
{
    internal class Stars
    {
        private Rectangle _formArea;
        private readonly List<Star> _stars;

        public Stars(Random random, Rectangle formArea)
        {
            this._formArea = formArea;
            _stars = new List<Star>();
            for (var i = 1; i < 300; i++)
                AddStar(random);
        }

        private void AddStar(Random random)
        {
            var height = _formArea.Height;
            var width = _formArea.Width;
            var location = new Point(random.Next(0, width), random.Next(0, height));
            var newStar = new Star(location, Brushes.Yellow);
            _stars.Add(newStar);
        }

        public Graphics Draw(Graphics graphics)
        {
            var starGraphics = graphics;
            foreach (var star in _stars)
                starGraphics.FillRectangle(star.Brush, star.Point.X, star.Point.Y, 1, 1);
            return starGraphics;
        }

        public void Twinkle(Random random)
        {
            // Remove 4 stars and randomly place 4 new ones
            _stars.RemoveRange(0, 4);
            for (var i = 0; i < 4; i++)
                AddStar(random);
        }
    }
}