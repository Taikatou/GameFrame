using System;
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
        public void TestMovement()
        {
            var expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<BaseMovable>();
            var startPoint = new Point(3, 4);
            var endPoint = new Point(3, 5);
            var toMove = new BaseMovable { Position = startPoint.ToVector2() };
            expiringSpatialHash.AddNode(startPoint, toMove);
            expiringSpatialHash.MoveNode(startPoint, endPoint, 200);
            Assert.IsTrue(expiringSpatialHash.CheckCollision(startPoint));
            Assert.IsTrue(expiringSpatialHash.CheckCollision(endPoint));
        }

        [TestMethod]
        public void TestUpdate()
        {
            var expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<BaseMovable>();
            var startPoint = new Point(3, 4);
            var endPoint = new Point(3, 5);
            var toMove = new BaseMovable {Position = startPoint.ToVector2()};
            expiringSpatialHash.AddNode(startPoint, toMove);
            expiringSpatialHash.MoveNode(startPoint, endPoint, 0);
            expiringSpatialHash.Update(new GameTime {ElapsedGameTime = new TimeSpan(0,0,0,1)});
            Assert.IsFalse(expiringSpatialHash.CheckCollision(startPoint));
            Assert.IsTrue(expiringSpatialHash.CheckCollision(endPoint));
        }
    }
}
