using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceInvaders
{
    internal class Stars
    {
        private Rectangle formArea;
        private readonly List<Star> stars;

        public Stars(Random random, Rectangle formArea)
        {
            this.formArea = formArea;
            stars = new List<Star>();
            for (var i = 1; i < 300; i++)
                addStar(random);
        }

        private void addStar(Random random)
        {
            var height = formArea.Height;
            var width = formArea.Width;
            var location = new Point(random.Next(0, width), random.Next(0, height));
            var newStar = new Star(location, Brushes.Yellow);
            stars.Add(newStar);
        }

        public Graphics Draw(Graphics graphics)
        {
            var starGraphics = graphics;
            foreach (var Star in stars)
                starGraphics.FillRectangle(Star.brush, Star.point.X, Star.point.Y, 1, 1);
            return starGraphics;
        }

        public void Twinkle(Random random)
        {
            // Remove 4 stars and randomly place 4 new ones
            stars.RemoveRange(0, 4);
            for (var i = 0; i < 4; i++)
                addStar(random);
        }
    }
}