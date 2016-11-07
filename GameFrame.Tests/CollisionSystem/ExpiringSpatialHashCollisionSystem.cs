using System;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.Movers;
using GameFrame.PathFinding.Heuristics;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameFrame.Tests.CollisionSystem
{
    [TestFixture]
    public class ExpiringSpatialHashCollisionSystem
    {
        [Test]
        public void TestMovement()
        {
            var expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<BaseMovable>(new EightWayPossibleMovement(new CrowDistance()));
            var startPoint = new Point(3, 4);
            var endPoint = new Point(3, 5);
            var toMove = new BaseMovable { Position = startPoint.ToVector2() };
            expiringSpatialHash.AddNode(startPoint, toMove);
            Assert.IsTrue(expiringSpatialHash.CheckCollision(startPoint));
            Assert.IsFalse(expiringSpatialHash.CheckCollision(endPoint));
        }

        [Test]
        public void TestUpdate()
        {
            var expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<BaseMovable>(new EightWayPossibleMovement(new CrowDistance()));
            var startPoint = new Point(3, 4);
            var endPoint = new Point(3, 5);
            var toMove = new BaseMovable {Position = startPoint.ToVector2()};
            expiringSpatialHash.AddNode(startPoint, toMove);
            expiringSpatialHash.Update(new GameTime {ElapsedGameTime = new TimeSpan(0,0,0,1)});
            Assert.IsTrue(expiringSpatialHash.CheckCollision(startPoint));
            Assert.IsFalse(expiringSpatialHash.CheckCollision(endPoint));
        }
    }
}
