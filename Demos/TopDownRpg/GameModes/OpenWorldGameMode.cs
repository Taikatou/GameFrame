using System.Collections.Generic;
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
        private readonly ContentManager _content;
        public readonly Camera2D Camera;
        public AbstractCollisionSystem CollisionSystem;
        public AbstractPathRenderer PathRenderer;
        private readonly IPossibleMovements _possibleMovements;

        public List<IUpdate> UpdateList;
        public List<IRenderable> RenderList;

        public OpenWorldGameMode(ViewportAdapter viewPort, IPossibleMovements possibleMovements)
        {
            _possibleMovements = possibleMovements;
            _content = ContentManagerFactory.RequestContentManager();
            UpdateList = new List<IUpdate>();
            RenderList = new List<IRenderable>();
            Camera = new Camera2D(viewPort) { Zoom = 2.0f };
            Map = _content.Load<TiledMap>("TopDownRpg/level01");
            var entity = new Entity(new Vector2(5, 5));
            var tileSize = new Vector2(Map.TileWidth, Map.TileHeight);
            var moverManager = new MoverManager();
            var collisionSystem = new CompositeAbstractCollisionSystem(_possibleMovements);
            var expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<Entity>(_possibleMovements);
            var entityRenderer = new EntityRenderer(_content, expiringSpatialHash,
                                                    entity, tileSize.ToPoint());
            var spatialHashMover = new SpatialHashMoverManager<Entity>(collisionSystem, expiringSpatialHash);
            var entityController = new EntityController(entity, moverManager);
            var texture = _content.Load<Texture2D>("TopDownRpg/Path");
            var endTexture = _content.Load<Texture2D>("TopDownRpg/BluePathEnd");

            collisionSystem.AddCollisionSystem(new TiledAbstractCollisionSystem(_possibleMovements, Map));
            collisionSystem.AddCollisionSystem(expiringSpatialHash);
            CollisionSystem = collisionSystem;

            AddClickController(entity, tileSize.ToPoint(), moverManager);
            spatialHashMover.Add(entity);
            PathRenderer = new PathRenderer(moverManager, entity, texture, endTexture, tileSize.ToPoint());
            UpdateList.Add(expiringSpatialHash);
            UpdateList.Add(new CameraTracker(Camera, entityRenderer));
            UpdateList.Add(entityController);
            UpdateList.Add(spatialHashMover);
            UpdateList.Add(moverManager);
            RenderList.Add(entityRenderer);

            var npc = new Entity(new Vector2(5, 4));
            spatialHashMover.Add(npc);
            var npcRenderer = new EntityRenderer(_content, expiringSpatialHash,
                                                 npc,
                                                 tileSize.ToPoint());
            RenderList.Add(npcRenderer);
            UpdateList.Add(new DelayTracker(npc, entity));
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
