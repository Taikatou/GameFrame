using System.Collections.Generic;
using System.Diagnostics;
using Demos.TopDownRpg.Factory;
using GameFrame;
using GameFrame.CollisionSystems;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.CollisionSystems.Tiled;
using GameFrame.Common;
using GameFrame.Content;
using GameFrame.Controllers;
using GameFrame.Controllers.Click;
using GameFrame.Controllers.Click.TouchScreen;
using GameFrame.Movers;
using GameFrame.PathFinding;
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

namespace Demos.TopDownRpg.GameModes
{
    public class OpenWorldGameMode : IGameMode
    {
        public TiledMap Map;
        private Vector2 _tileSize;
        private readonly ContentManager _content;
        public readonly Camera2D Camera;
        public AbstractCollisionSystem CollisionSystem;
        public AbstractPathRenderer PathRenderer;
        private readonly IPossibleMovements _possibleMovements;
        public Entity PlayerEntity;
        public Dictionary<Entity, EntityRenderer> EntityRenderersDict;
        private readonly RendererFactory _rendererFactory;
        private readonly ControllerFactory _controllerFactory;
        public List<IUpdate> UpdateList;
        public List<IRenderable> RenderList;
        private readonly ExpiringSpatialHashCollisionSystem<Entity> _expiringSpatialHash;

        public OpenWorldGameMode(ViewportAdapter viewPort, IPossibleMovements possibleMovements, Entity playerEntity, string worldName, RendererFactory renderFactory, ControllerFactory controllerFactory)
        {
            _rendererFactory = renderFactory;
            _controllerFactory = controllerFactory;
            EntityRenderersDict = new Dictionary<Entity, EntityRenderer>();
            _possibleMovements = possibleMovements;
            _content = ContentManagerFactory.RequestContentManager();
            UpdateList = new List<IUpdate>();
            RenderList = new List<IRenderable>();
            Camera = new Camera2D(viewPort) { Zoom = 2.0f };
            Map = _content.Load<TiledMap>($"TopDownRpg/{worldName}");
            PlayerEntity = playerEntity;
            _tileSize = new Vector2(Map.TileWidth, Map.TileHeight);
            var moverManager = new MoverManager();
            var collisionSystem = new CompositeAbstractCollisionSystem(_possibleMovements);
            _expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<Entity>(_possibleMovements);
            AddEntity(PlayerEntity);
            var spatialHashMover = new SpatialHashMoverManager<Entity>(collisionSystem, _expiringSpatialHash);
            var entityController = _controllerFactory.CreateEntityController(PlayerEntity, _possibleMovements, moverManager);
            var texture = _content.Load<Texture2D>("TopDownRpg/Path");
            var endTexture = _content.Load<Texture2D>("TopDownRpg/BluePathEnd");

            collisionSystem.AddCollisionSystem(new TiledCollisionSystem(_possibleMovements, Map, "Collision-Layer"));
            collisionSystem.AddCollisionSystem(_expiringSpatialHash);
            CollisionSystem = collisionSystem;

            AddClickController(PlayerEntity, _tileSize.ToPoint(), moverManager);
            spatialHashMover.Add(PlayerEntity);
            PathRenderer = new PathRenderer(moverManager, PlayerEntity, texture, endTexture, _tileSize.ToPoint());
            UpdateList.Add(_expiringSpatialHash);
            UpdateList.Add(entityController);
            UpdateList.Add(spatialHashMover);
            UpdateList.Add(moverManager);
            UpdateList.Add(new CameraTracker(Camera, EntityRenderersDict[PlayerEntity]));
            LoadEntities();
        }

        public void LoadEntities()
        {
            var entityObjects = Map.GetObjectGroup("Entity-Layer");
            foreach (var entityObject in entityObjects.Objects)
            {
                var position = entityObject.Position/_tileSize;
                var entity = new Entity(position);
                AddEntity(entity);
            }
        }

        public void AddEntity(Entity entity)
        {
            var entityRenderer = _rendererFactory.CreateEntityRenderer(_content, _expiringSpatialHash,
                                                    entity, _tileSize.ToPoint());
            _expiringSpatialHash.AddNode(entity.Position.ToPoint(), entity);
            RenderList.Add(entityRenderer);
            EntityRenderersDict[entity] = entityRenderer;
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
            var path = new AStarPathFinder(searchParams, _possibleMovements).FindPath();
            var pathMover = new PathMover(entity, new FinitePath(path));
            pathMover.OnCancelEvent += (sender, args) => entity.MovingDirection = new Vector2();
            moverManager.AddMover(pathMover);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var transformMatrix = Camera.GetViewMatrix();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
            Map.Draw(transformMatrix);
            foreach (var toRender in RenderList)
            {
                toRender.Draw(spriteBatch);
            }
            PathRenderer.Draw(spriteBatch);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            foreach (var toUpdate in UpdateList)
            {
                toUpdate.Update(gameTime);
            }
        }

        public void Dispose()
        {
            _content.Unload();
            _content.Dispose();
        }
    }
}
