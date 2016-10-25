using GameFrame;
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
    public class TopDownRpgScene : GameFrameScreen
    {
        public TiledMap Map;
        private readonly ContentManager _content;
        public readonly Camera2D Camera;
        public AbstractCollisionSystem AbstractCollisionSystem;
        public AbstractPathRenderer PathRenderer;
        private MoverManager _moverManager;
        private Entity _entity;
        private Vector2 _tileSize;
        private readonly SpriteBatch _spriteBatch;
        private EightWayPossibleMovement _possibleMovements;

        public TopDownRpgScene(ViewportAdapter viewPort, SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
            _content = ContentManagerFactory.RequestContentManager();
            Camera = new Camera2D(viewPort) {Zoom = 2.0f};
        }
        public override void LoadContent()
        {
            base.LoadContent();
            Map = _content.Load<TiledMap>("TopDownRpg/level01");
            _tileSize = new Vector2(Map.TileWidth, Map.TileHeight);
            _moverManager = new MoverManager();
            _possibleMovements = new EightWayPossibleMovement(new CrowDistance());
            var collisionSystem = new CompositeAbstractCollisionSystem(_possibleMovements);
            var expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<Entity>(_possibleMovements);
            var entityRenderer = new EntityRenderer(_content, expiringSpatialHash,
                                                    _entity = new Entity(new Vector2(5, 5)),
                                                    _tileSize.ToPoint());
            var spatialHashMover = new SpatialHashMoverManager<Entity>(collisionSystem, expiringSpatialHash);
            var entityController = new EntityController(_entity, _entity, _moverManager);
            var texture = _content.Load<Texture2D>("TopDownRpg/Path");
            var endTexture = _content.Load<Texture2D>("TopDownRpg/BluePathEnd");

            collisionSystem.AddCollisionSystem(new TiledAbstractCollisionSystem(_possibleMovements, Map));
            collisionSystem.AddCollisionSystem(expiringSpatialHash);
            AbstractCollisionSystem = collisionSystem;

            AddClickController(_entity, _tileSize.ToPoint(), _moverManager);
            spatialHashMover.Add(_entity);
            PathRenderer = new PathRenderer(_moverManager, _entity, texture, endTexture, _tileSize.ToPoint());
            UpdateList.Add(expiringSpatialHash);
            UpdateList.Add(new CameraTracker(Camera, entityRenderer));
            UpdateList.Add(entityController);
            UpdateList.Add(spatialHashMover);
            UpdateList.Add(_moverManager);
            RenderList.Add(entityRenderer);
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
            var searchParams = new SearchParameters(entity.Position.ToPoint(), endPoint, AbstractCollisionSystem, new Rectangle(new Point(), tileSize));
            var path = new AStarPathFinder(searchParams, _possibleMovements).FindPath();
            var pathMover = new PathMover(entity, new FinitePath(path));
            moverManager.AddMover(pathMover);
        }

        public override void Draw(GameTime gameTime)
        {
            var transformMatrix = Camera.GetViewMatrix();
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
            Map.Draw(transformMatrix);
            foreach (var toRender in RenderList)
            {
                toRender.Draw(_spriteBatch);
            }
            PathRenderer.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}
