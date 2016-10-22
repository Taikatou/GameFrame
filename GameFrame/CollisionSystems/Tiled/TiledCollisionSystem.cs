using Microsoft.Xna.Framework;
using MonoGame.Extended.Maps.Tiled;

namespace GameFrame.CollisionSystems.Tiled
{
    public class TiledCollisionSystem : ICollisionSystem
    {
        public TiledTileLayer TileCollisionLayer;

        public bool CheckCollision(Point p)
        {
            var tile = TileCollisionLayer.GetTile(p.X, p.Y);
            return !(tile == null || tile.Id == 0);
        }

        public TiledCollisionSystem(TiledMap map, string collisionLayerName="Collision-Layer")
        {
            TileCollisionLayer = map.GetLayer<TiledTileLayer>(collisionLayerName);
        }
    }
}
