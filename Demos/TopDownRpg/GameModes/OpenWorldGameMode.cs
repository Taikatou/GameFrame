using System;
using System.Collections.Generic;
using System.Linq;
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
using GameFrame.MediaAdapter;
using GameFrame.Controllers.GamePad;
using GameFrame.Controllers.KeyBoard;
using GameFrame.Controllers.SmartButton;
using GameFrame.Movers;
using GameFrame.PathFinding;
using GameFrame.PathFinding.PossibleMovements;
using GameFrame.Paths;
using GameFrame.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public List<IUpdate> UpdateList;
        public List<IRenderable> RenderList;
        private readonly ExpiringSpatialHashCollisionSystem<Entity> _expiringSpatialHash;
        private IAudioPlayer _audioAdapter;

        public OpenWorldGameMode(ViewportAdapter viewPort, IPossibleMovements possibleMovements, Entity playerEntity, string worldName, RendererFactory renderFactory, ControllerFactory controllerFactory)
        {
            //PlayMusic();
            _rendererFactory = renderFactory;
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
            var entityController = controllerFactory.CreateEntityController(PlayerEntity, _possibleMovements, moverManager);
            AddInteractionController(entityController);
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

        public void PlayMusic()
        {
            _audioAdapter = new AudioAdapter();
            _audioAdapter.Play("wav", "TopDownRPG/BirabutoKingdom");
            _audioAdapter.Pause();
            _audioAdapter.Resume();
        }

        public void AddInteractionController(BaseMovableController controller)
        {
            var runningButton = new List<IButtonAble> { new KeyButton(Keys.E), new GamePadButton(Buttons.A) };
            var smartButton = new CompositeSmartButton(runningButton)
            {
                OnButtonJustPressed = (sender, args) =>
                {
                    var interactTarget = (PlayerEntity.Position + PlayerEntity.FacingDirection).ToPoint();
                    Interact(interactTarget);
                }
            };
            controller.AddButton(smartButton);
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
            var entityRenderer = _rendererFactory.CreateEntityRenderer(_content, _expiringSpatialHash, entity, _tileSize.ToPoint());
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
                BeginMovingPlayerTo(endPoint.ToPoint(), entity, tileSize, moverManager);
            };
            var moveGesture = new SmartGesture(GestureType.Tap);
            moveGesture.GestureEvent += gesture =>
            {
                var endPoint = Camera.ScreenToWorld(gesture.Position);
                BeginMovingPlayerTo(endPoint.ToPoint(), entity, tileSize, moverManager);
            };
            clickController.TouchScreenControl.AddSmartGesture(moveGesture);
            UpdateList.Add(clickController);
        }

        public void BeginMovingPlayerTo(Point endPoint, Entity entity, Point tileSize, MoverManager moverManager)
        {
            endPoint /= tileSize;
            var moveTo = endPoint;
            var collision = _expiringSpatialHash.CheckCollision(moveTo);
            var valid = true;
            if (collision)
            {
                var heuristic = _possibleMovements.Heuristic;
                var startPosition = entity.Position.ToPoint();
                if (Math.Abs(heuristic.GetTraversalCost(startPosition, endPoint) - 1.0f) < 0.1f)
                {
                    Interact(endPoint);
                    valid = false;
                }
                else
                {
                    var alternatuvePositions = FourWayPossibleMovement.FourWayAdjacentLocations(moveTo);
                    var minCost = double.MaxValue;
                    foreach (var position in alternatuvePositions)
                    {
                        if (!CollisionSystem.CheckCollision(position))
                        {
                            var cost = heuristic.GetTraversalCost(startPosition, position);
                            if (cost < minCost)
                            {
                                minCost = cost;
                                moveTo = position;
                            }
                        }
                    }
                    if (moveTo == endPoint)
                    {
                        valid = false;
                    }
                }
            }
            else
            {
                valid = !CollisionSystem.CheckCollision(moveTo);
            }
            if (valid)
            {
                MovePlayerTo(moveTo, entity, tileSize, moverManager, collision, endPoint);
            }
        }

        public void Interact(Point interactTarget)
        {
            var validInteraction = FourWayPossibleMovement.FourWayAdjacentLocations(PlayerEntity.Position.ToPoint()).Contains(interactTarget);
            if (validInteraction)
            {
                PlayerEntity.FacingDirection = interactTarget.ToVector2() - PlayerEntity.Position;
                var interactWith = _expiringSpatialHash.ValueAt(interactTarget);
                interactWith?.Interact();
            }
        }

        public void MovePlayerTo(Point endPoint, Entity entity, Point tileSize, MoverManager moverManager, bool interact, Point interactWith)
        {
            var searchParams = new SearchParameters(entity.Position.ToPoint(), endPoint, CollisionSystem, new Rectangle(new Point(), tileSize));
            var path = new AStarPathFinder(searchParams, _possibleMovements).FindPath();
            var pathMover = new PathMover(entity, new FinitePath(path), new ExpiringSpatialHashMovementComplete<Entity>(_expiringSpatialHash, PlayerEntity));
            pathMover.OnCancelEvent += (sender, args) => entity.MovingDirection = new Vector2();
            if (interact)
            {
                pathMover.OnCompleteEvent += (sender, args) => Interact(interactWith);
            }
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
