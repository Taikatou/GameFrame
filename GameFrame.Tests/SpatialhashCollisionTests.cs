using System;
using GameFrame.CollisionSystems.SpatialHash;
using NUnit.Framework;
using Microsoft.Xna.Framework;

namespace GameFrame.Tests
{
    [TestFixture]
    public class SpatialHashCollisionTests
    {
        public bool CheckerBox(int i, int j)
        {
            return (i*j + j)%2 == 0;
        }

        [Test]
        public void SpatialHash()
        {
            var collisionSystem = new SpatialHashCollisionSystem<string>();
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    if (CheckerBox(i, j))
                    {
                        collisionSystem.AddNode(new Point(i, j), "");
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
    }
}