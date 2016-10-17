using GameFrame.CollisionSystems;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.CollisionSystems.Tiled;
using GameFrame.Common;
using GameFrame.Content;
using GameFrame.Controllers.Click;
using GameFrame.Controllers.Click.TouchScreen;
using GameFrame.Movers;
using GameFrame.PathFinding;
using GameFrame.PathFinding.Heuristics;
using GameFrame.PathFinding.PossibleMovements;
using GameFrame.Paths;
using GameFrame.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
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
        public EntityRenderer EntityRenderer;
        public PathRenderer PathRenderer;
        private MoverManager _moverManager;
        private Entity _entity;
        private Vector2 _tileSize;

        public TopDownRpgScene(ViewportAdapter viewPort)
        {
            _content = ContentManagerFactory.RequestContentManager();
            Camera = new Camera2D(viewPort) {Zoom = 2.0f};
        }
        public override void LoadScene()
        {
            var texture = _content.Load<Texture2D>("TopDownRpg/Path");
            PathRenderer = new PathRenderer(texture);
            var fileName = "TopDownRpg/level01";
            Map = _content.Load<TiledMap>(fileName);
            _tileSize = new Vector2(Map.TileWidth, Map.TileHeight);
            _entity = new Entity(new Vector2(5, 5));
            var collisionSystem = new CompositeCollisionSystem();
            var tileMapCollisionSystem = new TiledCollisionSystem(Map);
            var expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<Entity>();
            EntityRenderer = new EntityRenderer(_content, expiringSpatialHash, _entity, _tileSize.ToPoint());
            collisionSystem.AddCollisionSystem(tileMapCollisionSystem);
            collisionSystem.AddCollisionSystem(expiringSpatialHash);
            CollisionSystem = collisionSystem;
            var followCamera = new CameraTracker(Camera, EntityRenderer);
            var playerMover = new SpatialHashMoverManager<Entity>(collisionSystem, _entity, expiringSpatialHash);
            _moverManager = new MoverManager();
            var entityController = new EntityController(_entity, _entity, _moverManager);
            AddClickController(_entity, _tileSize.ToPoint(), _moverManager);
            UpdateList.Add(expiringSpatialHash);
            UpdateList.Add(followCamera);
            UpdateList.Add(entityController);
            UpdateList.Add(playerMover);
            UpdateList.Add(_moverManager);
        }

        public void AddClickController(Entity entity, Point tileSize, MoverManager moverManager)
        {
            var clickController = new ClickController();
            clickController.MouseControl.OnPressedEvent += (state, mouseState) =>
            {
                var endPoint = Camera.ScreenToWorld(mouseState.X, mouseState.Y);
                MovePlayerTo(endPoint.ToPoint(), entity, tileSize, moverManager);
            };
            var moveGesture = new SmartGesture(GestureType.Tap);
            moveGesture.GestureEvent += gesture =>
            {
                var endPoint = Camera.ScreenToWorld(gesture.Position);
                MovePlayerTo(endPoint.ToPoint(), entity, tileSize, moverManager);
            };
            clickController.TouchScreenControl.AddSmartGesture(moveGesture);
            UpdateList.Add(clickController);
        }

        public void MovePlayerTo(Point endPoint, Entity entity, Point tileSize, MoverManager moverManager)
        {
            endPoint /= tileSize;
            var searchParams = new SearchParameters(entity.Position.ToPoint(), endPoint, CollisionSystem, new Rectangle(new Point(), tileSize));
            var path = new AStarPathFinder(searchParams, new ManhattanDistance(), new FourWayPossibleMovement()).FindPath();
            var pathMover = new PathMover(entity, new FinitePath(path));
            moverManager.AddMover(pathMover);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var transformMatrix = Camera.GetViewMatrix();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
            Map.Draw(transformMatrix);
            EntityRenderer.Draw(spriteBatch);
            var points = _moverManager.PathPoints(_entity);
            PathRenderer.Draw(spriteBatch, points, _tileSize);
            spriteBatch.End();
        }
    }
}
