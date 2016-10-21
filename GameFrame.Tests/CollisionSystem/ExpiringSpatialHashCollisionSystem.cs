using System.Collections.Generic;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.Movers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace GameFrame.Tests.CollisionSystem
{
    [TestClass]
    public class ExpiringSpatialHashCollisionSystem
    {
        [TestMethod]
        public void ExpiringCheckerBoxTest()
        {
            var collisionSystem = new ExpiringSpatialHashCollisionSystem<BaseMovable>(10);
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    if (Common.CheckerBox(i, j))
                    {
                        collisionSystem.AddNode(new Point(i, j), new BaseMovable());
                    }
                }
            }
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    Assert.AreEqual(Common.CheckerBox(i, j), collisionSystem.CheckCollision(i, j));
                }
            }
        }

        [TestMethod]
        public void ExpiringPointListTest()
        {
            var collisionSystem = new ExpiringSpatialHashCollisionSystem<BaseMovable>(10);
            var points = new List<Point> { new Point(3, 4), new Point(4, 5), new Point(7, 8) };
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
