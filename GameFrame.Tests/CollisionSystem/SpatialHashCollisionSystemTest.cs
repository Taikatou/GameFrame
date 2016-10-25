using System.Collections.Generic;
using System.Diagnostics;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.Movers;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace GameFrame.Tests.CollisionSystem
{
    [TestClass]
    public class SpatialHashCollisionSystemTest
    {
        [TestMethod]
        public void CheckerBoxTest()
        {
            var collisionSystem = new SpatialHashCollisionSystem<BaseMovable>(new FourWayPossibleMovement());
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    if (Common.CheckerBox(i, j))
                    {
                        var point = new Point(i, j);
                        Debug.WriteLine(point);
                        collisionSystem.AddNode(point, new BaseMovable());
                    }
                }
            }
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var p = new Point(i, j);
                    Assert.AreEqual(Common.CheckerBox(i, j), collisionSystem.CheckCollision(p));
                }
            }
        }

        [TestMethod]
        public void PointListTest()
        {
            var collisionSystem = new SpatialHashCollisionSystem<BaseMovable>(new FourWayPossibleMovement());
            var points = new List<Point> {new Point(3, 4), new Point(4, 5), new Point(7, 8)};
            var notPoints = new List<Point> { new Point(13, 14), new Point(14, 15), new Point(17, 18) };
            foreach (var point in points)
            {
                collisionSystem.AddNode(point, new BaseMovable());
            }
            foreach (var point in points)
            {
                Assert.IsTrue(collisionSystem.CheckCollision(point));
            }
            foreach (var point in notPoints)
            {
                Assert.IsFalse(collisionSystem.CheckCollision(point));
            }
        }
    }
}
