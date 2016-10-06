using Microsoft.Xna.Framework;
using MonoGame.Extended.Maps.Tiled;

namespace GameFrame.CollisionSystems.Tiled
{
    public class TiledCollisionSystem : ICollisionSystem
    {
        public TiledTileLayer TileCollisionLayer;

        public bool CheckCollision(int x, int y)
        {
            var tile = TileCollisionLayer.GetTile(x, y);
            return !(tile == null || tile.Id == 0);
        }

        public bool CheckCollision(Point position)
        {
            return CheckCollision(position.X, position.Y);
        }

        public TiledCollisionSystem(TiledMap map, string collisionLayerName="Collision-Layer")
        {
            TileCollisionLayer = map.GetLayer<TiledTileLayer>(collisionLayerName);
        }
    }
}
