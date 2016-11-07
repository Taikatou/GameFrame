using System.Drawing;

namespace SpaceInvaders
{
    internal struct Star
    {
        public Point point;
        public Brush brush;

        public Star(Point point, Brush brush)
        {
            this.point = point;
            this.brush = brush;
        }
    }
}