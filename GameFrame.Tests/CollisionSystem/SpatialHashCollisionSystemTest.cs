using System.Collections.Generic;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.Movers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace GameFrame.Tests.CollisionSystem
{
    [TestClass]
    public class SpatialHashCollisionSystemTest
    {
        public bool CheckerBox(int i, int j)
        {
            return i + j == 0;
        }

        [TestMethod]
        public void CheckerBoxTest()
        {
            var collisionSystem = new SpatialHashCollisionSystem<BaseMovable>(10);
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    if (CheckerBox(i, j))
                    {
                        collisionSystem.AddNode(new Point(i, j), new BaseMovable());
                    }
                }
            }
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    Assert.AreEqual(CheckerBox(i, j), collisionSystem.CheckCollision(i, j));
                }
            }
        }

        [TestMethod]
        public void PointListTest()
        {
            var collisionSystem = new SpatialHashCollisionSystem<BaseMovable>(10);
            var points = new List<Point> {new Point(3, 4), new Point(4, 5), new Point(7, 8)};
            foreach (var point in points)
            {
                collisionSystem.AddNode(point, new BaseMovable());
            }
            foreach (var point in points)
            {
                Assert.IsTrue(collisionSystem.CheckCollision(point));
            }
        }
    }
}
