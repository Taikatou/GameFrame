using GameFrame.CollisionSystems;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.PathFinding
{
    public class SearchParameters
    {
        public Point StartLocation { get; set; }
        public Point EndLocation { get; set; }
        public AbstractCollisionSystem AbstractCollisionSystem { get; set; }
        public Size Space { get; set; }

        public SearchParameters(Point startLocation, Point endLocation, AbstractCollisionSystem abstractCollisionSystem, Size searchSpace)
        {
            Space = searchSpace;
            StartLocation = startLocation;
            EndLocation = endLocation;
            AbstractCollisionSystem = abstractCollisionSystem;
        }
    }
}
