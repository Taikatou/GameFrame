using GameFrame.CollisionSystems;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.Movers;
using GameFrame.PathFinding;
using GameFrame.PathFinding.Heuristics;
using GameFrame.PathFinding.PossibleMovements;
using GameFrame.Paths;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace GameFrame.Tests.Paths
{
    [TestClass]
    public class PathTest
    {

        [TestMethod]
        public void PathsFinderTest()
        {
            var entity = new BaseMovable();
            var endPoint = new Point();
            
            var collisionSystem = new CompositeAbstractCollisionSystem(new EightWayPossibleMovement(new CrowDistance()));
            var tileSize = new Point();
            var searchParams = new SearchParameters(entity.Position.ToPoint(), endPoint, collisionSystem, new Rectangle(new Point(), tileSize));
            var path = new AStarPathFinder(searchParams, new EightWayPossibleMovement(new CrowDistance()));
            Assert.AreNotSame(path.ClosedNodes, path.MapNodes);
        }

        [TestMethod]
        public void PathTypesTest()
        {
            var eightWayPossibleMovement = new EightWayPossibleMovement(new CrowDistance());
            PossibleMovementWrapper wrapper = new PossibleMovementWrapper(eightWayPossibleMovement);
            Assert.AreEqual(wrapper.Heuristic,eightWayPossibleMovement.Heuristic);

            eightWayPossibleMovement = new EightWayPossibleMovement(new DiagonalDistance());
            wrapper = new PossibleMovementWrapper(eightWayPossibleMovement);
            Assert.AreEqual(wrapper.Heuristic, eightWayPossibleMovement.Heuristic);

            eightWayPossibleMovement = new EightWayPossibleMovement(new ManhattanDistance());
            wrapper = new PossibleMovementWrapper(eightWayPossibleMovement);
            Assert.AreEqual(wrapper.Heuristic, eightWayPossibleMovement.Heuristic);


            var fourWayPossibleMovement = new FourWayPossibleMovement(new CrowDistance());
            wrapper = new PossibleMovementWrapper(fourWayPossibleMovement);
            Assert.AreEqual(wrapper.Heuristic, fourWayPossibleMovement.Heuristic);

            fourWayPossibleMovement = new FourWayPossibleMovement(new DiagonalDistance());
            wrapper = new PossibleMovementWrapper(fourWayPossibleMovement);
            Assert.AreEqual(wrapper.Heuristic, fourWayPossibleMovement.Heuristic);

            fourWayPossibleMovement = new FourWayPossibleMovement(new ManhattanDistance());
            wrapper = new PossibleMovementWrapper(fourWayPossibleMovement);
            Assert.AreEqual(wrapper.Heuristic, fourWayPossibleMovement.Heuristic);
        }
    }
}
