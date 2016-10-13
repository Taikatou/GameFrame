using GameFrame.CollisionSystems;
using Microsoft.Xna.Framework;

namespace GameFrame.PathFinding
{
    public class SearchParameters
    {
        public Point StartLocation { get; set; }
        public Point EndLocation { get; set; }
        public ICollisionSystem CollisionSystem { get; set; }
        public Rectangle Space { get; set; }

        public SearchParameters(Point startLocation, Point endLocation, ICollisionSystem collisionSystem, Rectangle space)
        {
            Space = space;
            StartLocation = startLocation;
            EndLocation = endLocation;
            CollisionSystem = collisionSystem;
        }
    }
}
