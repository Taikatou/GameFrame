using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Maps.Tiled;

namespace GameFrame.CollisionSystems.Tiled
{
    public class TiledCollisionSystem : AbstractCollisionSystem
    {
        public TiledTileLayer TileCollisionLayer;
        public Point TileSize;
        public override bool CheckCollision(Point startPoint)
        {
            var xPos = startPoint.X/TileSize.X;
            var yPos = startPoint.Y/TileSize.Y;
            var tile = TileCollisionLayer.GetTile(xPos, yPos);
            return !(tile == null || tile.Id == 0);
        }

        public TiledCollisionSystem(IPossibleMovements possibleMovements, TiledMap map, string collisionLayerName) : base(possibleMovements)
        {
            TileCollisionLayer = map.GetLayer<TiledTileLayer>(collisionLayerName);
            TileSize = new Point(map.TileWidth, map.TileHeight);
        }
    }
}
