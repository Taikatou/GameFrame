using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;

namespace GameFrame.CollisionSystems.Tiled
{
    public class TiledCollisionSystem : AbstractCollisionSystem
    {
        public TiledTileLayer TileCollisionLayer;

        public override bool CheckCollision(Point startPoint)
        {
            var tile = TileCollisionLayer.GetTile(startPoint.X, startPoint.Y);
            return !(tile == null || tile.Id == 0);
        }

        public TiledCollisionSystem(IPossibleMovements possibleMovements, TiledMap map, string collisionLayerName) : base(possibleMovements)
        {
            TileCollisionLayer = map.GetLayer<TiledTileLayer>(collisionLayerName);
        }
    }
}
