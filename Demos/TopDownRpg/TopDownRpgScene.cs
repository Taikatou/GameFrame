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
    public class TopDownRpgScene : IDemoScene
    {
        public TiledMap Map;
        private ContentManager _content;
        public readonly Camera2D Camera;
        public ICollisionSystem CollisionSystem;

        public TopDownRpgScene(ViewportAdapter viewPort)
        {
            _content = ContentManagerFactory.RequestContentManager();
            Camera = new Camera2D(viewPort);
        }
        public void LoadScene()
        {
            var fileName = "TopDownRpg/level01";
            Map = _content.Load<TiledMap>(fileName);
            var collisionSystem = new CompositeCollisionSystem();
            var tileMapCollisionSystem = new TiledCollisionSystem(Map);
            var expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<Entity>();
            collisionSystem.AddCollisionSystem(tileMapCollisionSystem);
            collisionSystem.AddCollisionSystem(expiringSpatialHash);
            CollisionSystem = collisionSystem;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var transformMatrix = Camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);
            Map.Draw(transformMatrix);
            spriteBatch.End();
        }
    }
}
