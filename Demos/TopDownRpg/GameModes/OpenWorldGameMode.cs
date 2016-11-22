using System;
using System.Collections.Generic;
using System.Linq;
using Demos.TopDownRpg.Entities;
using GameFrame;
using GameFrame.CollisionSystems;
using GameFrame.CollisionSystems.SpatialHash;
using GameFrame.CollisionSystems.Tiled;
using GameFrame.Common;
using GameFrame.Content;
using GameFrame.Controllers;
using GameFrame.Controllers.Click;
using GameFrame.Controllers.Click.TouchScreen;
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
    public delegate void AddEntity(Entity entity);

    public delegate void RemoveEntity(Entity entity);
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
        public List<IUpdate> UpdateList;
        public List<IRenderable> RenderList;
        private readonly ExpiringSpatialHashCollisionSystem<Entity> _expiringSpatialHash;
        private readonly EntityStoryBoxDialog _entityDialogBox;
        private readonly SpatialHashMoverManager<Entity> _spatialHashMover;
        private readonly EntityManager _entityManager;
        private readonly MoverManager _moverManager;
        private readonly StoryEngine _storyEngine;

        public void Move(Entity entity, Point p)
        {
            BeginMovingEntityTo(p, entity, _tileSize.ToPoint(), _moverManager);
        }

        public OpenWorldGameMode(ViewportAdapter viewPort, IPossibleMovements possibleMovements, Entity playerEntity, string worldName, ControllerFactory controllerFactory, EntityManager entityManager, StoryEngine storyEngine)
        {
            _storyEngine = storyEngine;
            _entityManager = entityManager;
            EntityRenderersDict = new Dictionary<Entity, EntityRenderer>();
            _possibleMovements = possibleMovements;
            _content = ContentManagerFactory.RequestContentManager();
            UpdateList = new List<IUpdate>();
            RenderList = new List<IRenderable>();
            Camera = new Camera2D(viewPort) { Zoom = 2.0f };
            Map = _content.Load<TiledMap>($"TopDownRpg/{worldName}");
            PlayerEntity = playerEntity;
            _tileSize = new Vector2(Map.TileWidth, Map.TileHeight);
            _moverManager = new MoverManager();
            var collisionSystem = new CompositeAbstractCollisionSystem(_possibleMovements);
            _expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<Entity>(_possibleMovements);
            _spatialHashMover = new SpatialHashMoverManager<Entity>(collisionSystem, _expiringSpatialHash);
            AddEntity(PlayerEntity);
            var entityController = controllerFactory.CreateEntityController(PlayerEntity, _possibleMovements, _moverManager);
            AddInteractionController(entityController, controllerFactory, _moverManager);
            var texture = _content.Load<Texture2D>("TopDownRpg/Path");
            var endTexture = _content.Load<Texture2D>("TopDownRpg/BluePathEnd");

            collisionSystem.AddCollisionSystem(new TiledCollisionSystem(_possibleMovements, Map, "Collision-Layer"));
            collisionSystem.AddCollisionSystem(_expiringSpatialHash);
            CollisionSystem = collisionSystem;
            AddClickController(PlayerEntity, _tileSize.ToPoint(), _moverManager);
            PathRenderer = new PathRenderer(_moverManager, PlayerEntity, texture, endTexture, _tileSize.ToPoint());
            UpdateList.Add(_expiringSpatialHash);
            UpdateList.Add(entityController);
            UpdateList.Add(_spatialHashMover);
            UpdateList.Add(_moverManager);
            UpdateList.Add(new CameraTracker(Camera, EntityRenderersDict[PlayerEntity]));
            LoadEntities();
            _storyEngine.LoadWorld(AddEntity, RemoveEntity, worldName);
            var dialogFont = _content.Load<SpriteFont>("dialog");
            _entityDialogBox = new EntityStoryBoxDialog(dialogFont, playerEntity);
            UpdateList.Add(_entityDialogBox);
        }

        public void AddInteractionController(BaseMovableController controller, ControllerFactory controllerFactory, MoverManager moverManager)
        {
            var runningButton = new List<IButtonAble> { new KeyButton(Keys.E, controllerFactory.KeyboardUpdater), new GamePadButton(Buttons.A) };
            var smartButton = new CompositeSmartButton(runningButton)
            {
                OnButtonJustPressed = (sender, args) =>
                {
                    if (!_entityDialogBox.Interact())
                    {
                        var interactTarget = (PlayerEntity.Position + PlayerEntity.FacingDirection).ToPoint();
                        Interact(interactTarget, moverManager);
                    }
                }
            };
            controller.AddButton(smartButton);
        }

        public void LoadEntities()
        {
            var entityObjects = Map.GetObjectGroup("Entity-Layer");
            if (entityObjects != null)
            {
                foreach (var entityObject in entityObjects.Objects)
                {
                    var position = entityObject.Position / _tileSize;
                    var entity = _entityManager.Import(entityObject.Name);
                    entity.Position = position;
                    AddEntity(entity);
                }
            }
        }

        public void RemoveEntity(Entity entity)
        {
            var position = entity.Position.ToPoint();
            _expiringSpatialHash.RemoveNode(position);
            var entityRenderer = EntityRenderersDict[entity];
            RenderList.Remove(entityRenderer);
            EntityRenderersDict.Remove(entity);
            _spatialHashMover.Remove(entity);
        }

        public void AddEntity(Entity entity)
        {
            var entityRenderer = new EntityRenderer(_content, _expiringSpatialHash, entity, _tileSize.ToPoint());
            _expiringSpatialHash.AddNode(entity.Position.ToPoint(), entity);
            RenderList.Add(entityRenderer);
            EntityRenderersDict[entity] = entityRenderer;
            _spatialHashMover.Add(entity);
        }

        public void AddClickController(Entity entity, Point tileSize, MoverManager moverManager)
        {
            var clickController = new ClickController();
            clickController.MouseControl.OnPressedEvent += (state, mouseState) =>
            {
                if (!_entityDialogBox.Interact(mouseState.Position))
                {
                    var endPoint = Camera.ScreenToWorld(mouseState.X, mouseState.Y).ToPoint();
                    BeginMovingEntityTo(endPoint / tileSize, entity, tileSize, moverManager);
                }
            };
            var moveGesture = new SmartGesture(GestureType.Tap);
            moveGesture.GestureEvent += gesture =>
            {
                if (!_entityDialogBox.Interact(gesture.Position.ToPoint()))
                {
                    var endPoint = Camera.ScreenToWorld(gesture.Position).ToPoint();
                    BeginMovingEntityTo(endPoint / tileSize, entity, tileSize, moverManager);
                }
            };
            clickController.TouchScreenControl.AddSmartGesture(moveGesture);
            UpdateList.Add(clickController);
        }

        public void BeginMovingEntityTo(Point endPoint, Entity entity, Point tileSize, MoverManager moverManager)
        {
            var mapContainsEndPoint = endPoint.X <= Map.Width && endPoint.Y <= Map.Height;
            if (mapContainsEndPoint)
            {
                var moveTo = endPoint;
                var collision = _expiringSpatialHash.CheckCollision(moveTo);
                var valid = true;
                if (collision)
                {
                    var heuristic = _possibleMovements.Heuristic;
                    var startPosition = entity.Position.ToPoint();
                    if (Math.Abs(heuristic.GetTraversalCost(startPosition, endPoint) - 1.0f) < 0.1f)
                    {
                        Interact(endPoint, moverManager);
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
                    MoveEntityTo(moveTo, entity, tileSize, moverManager, collision, endPoint);
                }
            }
        }

        public void Interact(Point interactTarget, MoverManager moverManager)
        {
            var validInteraction = FourWayPossibleMovement.FourWayAdjacentLocations(PlayerEntity.Position.ToPoint()).Contains(interactTarget);
            if (validInteraction)
            {
                PlayerEntity.FacingDirection = interactTarget.ToVector2() - PlayerEntity.Position;
                var interactWith = _expiringSpatialHash.ValueAt(interactTarget);
                if (interactWith != null)
                {
                    var story = interactWith.Interact();
                    story.Continue();
                    _entityDialogBox.StartStory(story, interactWith);
                }
            }
        }

        public void MoveEntityTo(Point endPoint, Entity entity, Point tileSize, MoverManager moverManager, bool interact = false, Point? interactWith = null)
        {
            var searchParams = new SearchParameters(entity.Position.ToPoint(), endPoint, CollisionSystem, new Rectangle(new Point(), tileSize));
            var path = new AStarPathFinder(searchParams, _possibleMovements).FindPath();
            var pathMover = new PathMover(entity, new FinitePath(path), new ExpiringSpatialHashMovementComplete<Entity>(_expiringSpatialHash, PlayerEntity));
            pathMover.OnCancelEvent += (sender, args) => entity.MovingDirection = new Vector2();
            if (interact && interactWith != null)
            {
                pathMover.OnCompleteEvent += (sender, args) => Interact(interactWith.Value, moverManager);
            }
            moverManager.AddMover(pathMover);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var transformMatrix = Camera.GetViewMatrix();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix,
                              sortMode: SpriteSortMode.BackToFront, depthStencilState: DepthStencilState.Default);
            Map.Draw(transformMatrix);
            foreach (var toRender in RenderList)
            {
                if (Camera.Contains(toRender.Area) != ContainmentType.Disjoint)
                {
                    toRender.Draw(spriteBatch);
                }
            }
            PathRenderer.Draw(spriteBatch);
            spriteBatch.End();
            _entityDialogBox.Draw(spriteBatch);
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
