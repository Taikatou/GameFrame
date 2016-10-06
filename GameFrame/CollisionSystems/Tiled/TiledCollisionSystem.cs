using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Maps.Tiled;

namespace GameFrame.CollisionSystems.Tiled
{
    public class TiledCollisionSystem : ICollisionSystem
    {
        public TiledMap Map;
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

        public TiledCollisionSystem(string mapName, ContentManager content)
        {
            string fileName = $"TileMaps/{mapName}";
            Map = content.Load<TiledMap>(fileName);
            TileCollisionLayer = Map.GetLayer<TiledTileLayer>("Collision-Layer");
        }
    }
}
