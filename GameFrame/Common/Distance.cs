using System;
using Microsoft.Xna.Framework;

namespace GameFrame.Common
{
    public class Distance
    {
        public static double GetDistance(Vector2 startPoint, Vector2 endPoint)
        {
            var deltaX = Math.Abs(startPoint.X - endPoint.X);
            var deltaY = Math.Abs(startPoint.Y - endPoint.Y);
            var multi = deltaX*deltaX + deltaY*deltaY;
            return Math.Sqrt(multi);
        }
    }
}
