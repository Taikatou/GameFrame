using System.Drawing;
using Microsoft.Xna.Framework;

namespace Demos.Space_Invaders
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