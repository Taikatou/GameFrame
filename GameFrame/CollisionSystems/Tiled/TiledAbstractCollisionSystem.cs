using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Maps.Tiled;

namespace GameFrame.CollisionSystems.Tiled
{
    public class TiledAbstractCollisionSystem : AbstractCollisionSystem
    {
        public TiledTileLayer TileCollisionLayer;

        public override bool CheckCollision(Point startPoint)
        {
            var tile = TileCollisionLayer.GetTile(startPoint.X, startPoint.Y);
            return !(tile == null || tile.Id == 0);
        }

        public TiledAbstractCollisionSystem(IPossibleMovements possibleMovements, TiledMap map, string collisionLayerName="Collision-Layer") : base(possibleMovements)
        {
            TileCollisionLayer = map.GetLayer<TiledTileLayer>(collisionLayerName);
        }
    }
}
