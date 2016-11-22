using GameFrame.CollisionSystems;
using GameFrame.Movers;
using GameFrame.PathFinding;
using GameFrame.PathFinding.Heuristics;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Assert = NUnit.Framework.Assert;

namespace GameFrame.Tests.Paths
{
    [TestClass]
    public class PathTest
    {

        [TestMethod]
        public void PathsFinderTestCrowDistance()
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
        public void PathsFinderTestDiagonalDistance()
        {
            var entity = new BaseMovable();
            var endPoint = new Point();

            var collisionSystem = new CompositeAbstractCollisionSystem(new EightWayPossibleMovement(new DiagonalDistance()));
            var tileSize = new Point();
            var searchParams = new SearchParameters(entity.Position.ToPoint(), endPoint, collisionSystem, new Rectangle(new Point(), tileSize));
            var path = new AStarPathFinder(searchParams, new EightWayPossibleMovement(new DiagonalDistance()));
            Assert.AreNotSame(path.ClosedNodes, path.MapNodes);
        }


        [TestMethod]
        public void PathsFinderTestManhattanDistance()
        {
            var entity = new BaseMovable();
            var endPoint = new Point();

            var collisionSystem = new CompositeAbstractCollisionSystem(new EightWayPossibleMovement(new ManhattanDistance()));
            var tileSize = new Point();
            var searchParams = new SearchParameters(entity.Position.ToPoint(), endPoint, collisionSystem, new Rectangle(new Point(), tileSize));
            var path = new AStarPathFinder(searchParams, new EightWayPossibleMovement(new ManhattanDistance()));
            Assert.AreNotSame(path.ClosedNodes, path.MapNodes);
        }

        [TestMethod]
        public void PathTypesTest()
        {
            var p1 = new Point(9,5);
            var p2 = new Point(10,4);

            var eightWayPossibleMovement = new EightWayPossibleMovement(new CrowDistance());
            eightWayPossibleMovement.GetAdjacentLocations(p2);
            eightWayPossibleMovement.PositionsToCheck(p1, p2);

            PossibleMovementWrapper wrapper = new PossibleMovementWrapper(eightWayPossibleMovement);
            wrapper.PositionsToCheck(p1, p2);
            wrapper.GetAdjacentLocations(p1);
            Assert.AreEqual(wrapper.Heuristic,eightWayPossibleMovement.Heuristic);

            eightWayPossibleMovement = new EightWayPossibleMovement(new DiagonalDistance());
            wrapper = new PossibleMovementWrapper(eightWayPossibleMovement);
            Assert.AreEqual(wrapper.Heuristic, eightWayPossibleMovement.Heuristic);

            eightWayPossibleMovement = new EightWayPossibleMovement(new ManhattanDistance());
            wrapper = new PossibleMovementWrapper(eightWayPossibleMovement);
            Assert.AreEqual(wrapper.Heuristic, eightWayPossibleMovement.Heuristic);


            var fourWayPossibleMovement = new FourWayPossibleMovement(new CrowDistance());
            fourWayPossibleMovement.PositionsToCheck(p1, p2);
            fourWayPossibleMovement.GetAdjacentLocations(p2);

            wrapper = new PossibleMovementWrapper(fourWayPossibleMovement);
            wrapper.PositionsToCheck(p1, p2);
            wrapper.GetAdjacentLocations(p1);
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
