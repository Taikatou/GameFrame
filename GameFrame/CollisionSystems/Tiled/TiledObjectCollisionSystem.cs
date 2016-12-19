using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace GameFrame.CollisionSystems.Tiled
{
    public class TiledObjectCollisionSystem : AbstractCollisionSystem
    {
        private readonly SpatialHashCollisionSystem<TiledObject> _spatialHash;
        public TiledObjectCollisionSystem(IPossibleMovements possibleMovements, TiledMap map, Point tileSize, string objectLayer) : base(possibleMovements)
        {
            var teleporters = map.GetObjectGroup(objectLayer);
            _spatialHash = new SpatialHashCollisionSystem<TiledObject>(possibleMovements);
            foreach(var teleporter in teleporters.Objects)
            {
                var point = teleporter.Position.ToPoint() / tileSize;
                _spatialHash.AddNode(point, teleporter);
            }
        }

        public override bool CheckCollision(Point startPoint)
        {
            return _spatialHash.CheckCollision(startPoint);
        }

        public TiledObject GetObjectAt(Point point)
        {
            return _spatialHash.ValueAt(point);
        }
    }
}
