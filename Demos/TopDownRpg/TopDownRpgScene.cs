using GameFrame.CollisionSystems;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.CollisionSystems.Tiled;
using GameFrame.Common;
using GameFrame.Content;
using GameFrame.Movers;
using GameFrame.PathFinding;
using GameFrame.PathFinding.Heuristics;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.ViewportAdapters;
using GameFrame.MediaAdapter;

namespace Demos.TopDownRpg
{
    public class TopDownRpgScene : AbstractScene
    {
        public TiledMap Map;
        private readonly ContentManager _content;
        public readonly Camera2D Camera;
        public ICollisionSystem CollisionSystem;
        public EntityRenderer EntityRenderer;

        public TopDownRpgScene(ViewportAdapter viewPort)
        {
            _content = ContentManagerFactory.RequestContentManager();
            Camera = new Camera2D(viewPort) {Zoom = 2.0f};
        }
        public override void LoadScene()
        {
            var fileName = "TopDownRpg/level01";
            Map = _content.Load<TiledMap>(fileName);
            var tileSize = new Point(Map.TileWidth, Map.TileHeight);
            var entity = new Entity(new Vector2(5, 5));
            var collisionSystem = new CompositeCollisionSystem();
            var tileMapCollisionSystem = new TiledCollisionSystem(Map);
            var expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<Entity>();
            EntityRenderer = new EntityRenderer(_content, expiringSpatialHash, entity, tileSize);
            collisionSystem.AddCollisionSystem(tileMapCollisionSystem);
            collisionSystem.AddCollisionSystem(expiringSpatialHash);
            CollisionSystem = collisionSystem;
            var followCamera = new CameraTracker(Camera, EntityRenderer);
            var playerMover = new SpatialHashMoverManager<Entity>(collisionSystem, entity, expiringSpatialHash);
            var entityController = new EntityController(entity, entity, expiringSpatialHash);
            var searchParams = new SearchParameters(entity.Position.ToPoint(), new Point(5, 7), CollisionSystem, new Rectangle(new Point(), tileSize));
            var path = new AStarPathFinder(searchParams, new ManhattanDistance(), new FourWayPossibleMovement()).FindPath();
            var pathMover = new PathMover(entity, path);
            UpdateList.Add(expiringSpatialHash);
            UpdateList.Add(followCamera);
            UpdateList.Add(entityController);
            UpdateList.Add(playerMover);
            UpdateList.Add(pathMover);
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
