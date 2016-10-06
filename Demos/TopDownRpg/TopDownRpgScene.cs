using Demos.Platformer;
using GameFrame.CollisionSystems;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.CollisionSystems.Tiled;
using GameFrame.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.TopDownRpg
{
    public class TopDownRpgScene : AbstractScene
    {
        public TiledMap Map;
        private readonly ContentManager _content;
        public readonly Camera2D Camera;
        public ICollisionSystem CollisionSystem;
        public ShittyEntityRenderer EntityRenderer;

        public TopDownRpgScene(ViewportAdapter viewPort)
        {
            _content = ContentManagerFactory.RequestContentManager();
            Camera = new Camera2D(viewPort);
        }
        public override void LoadScene()
        {
            var fileName = "TopDownRpg/level01";
            Map = _content.Load<TiledMap>(fileName);
            var tileSize = new Point(Map.TileWidth, Map.TileHeight);
            EntityRenderer = new ShittyEntityRenderer(_content, new Point(5, 5), tileSize);
            var collisionSystem = new CompositeCollisionSystem();
            var tileMapCollisionSystem = new TiledCollisionSystem(Map);
            var expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<Entity>();
            collisionSystem.AddCollisionSystem(tileMapCollisionSystem);
            collisionSystem.AddCollisionSystem(expiringSpatialHash);
            CollisionSystem = collisionSystem;
            UpdateList.Add(expiringSpatialHash);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var transformMatrix = Camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);
            Map.Draw(transformMatrix);
            EntityRenderer.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
