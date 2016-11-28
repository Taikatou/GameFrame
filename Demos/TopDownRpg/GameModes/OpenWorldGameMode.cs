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
using GameFrame.Ink;
using GameFrame.Movers;
using GameFrame.PathFinding;
using GameFrame.PathFinding.PossibleMovements;
using GameFrame.Paths;
using GameFrame.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.TopDownRpg.GameModes
{
    public delegate void AddEntity(Entity entity);

    public delegate void RemoveEntity(Entity entity);

    public delegate void Say(GameFrameStory story);

    public class OpenWorldGameMode : AbstractRpgGameMode
    {
        public TiledMap Map;
        private Vector2 _tileSize;
        private readonly ContentManager _content;
        public readonly Camera2D Camera;
        public AbstractCollisionSystem CollisionSystem;
        public AbstractPathRenderer PathRenderer;
        private readonly IPossibleMovements _possibleMovements;
        public Dictionary<Entity, AbstractEntityRenderer> EntityRenderersDict;
        public List<IRenderable> RenderList;
        private readonly ExpiringSpatialHashCollisionSystem<Entity> _expiringSpatialHash;
        private readonly SpatialHashMoverManager<Entity> _spatialHashMover;
        private readonly EntityManager _entityManager;
        private readonly MoverManager _moverManager;
        public Entity PlayerEntity => Entities.PlayerEntity.Instance;

        public OpenWorldGameMode(ViewportAdapter viewPort, IPossibleMovements possibleMovements, string worldName, ControllerFactory controllerFactory, EntityManager entityManager, StoryEngine storyEngine)
        {
            _entityManager = entityManager;
            EntityRenderersDict = new Dictionary<Entity, AbstractEntityRenderer>();
            _possibleMovements = possibleMovements;
            _content = ContentManagerFactory.RequestContentManager();
            RenderList = new List<IRenderable>();
            Camera = new Camera2D(viewPort) { Zoom = 2.0f };
            Map = _content.Load<TiledMap>($"TopDownRpg/{worldName}");
            _tileSize = new Vector2(Map.TileWidth, Map.TileHeight);
            _moverManager = new MoverManager();
            var collisionSystem = new CompositeAbstractCollisionSystem(_possibleMovements);
            _expiringSpatialHash = new ExpiringSpatialHashCollisionSystem<Entity>(_possibleMovements);
            _spatialHashMover = new SpatialHashMoverManager<Entity>(collisionSystem, _expiringSpatialHash);
            AddPlayer();
            var entityController = controllerFactory.CreateEntityController(PlayerEntity, _possibleMovements, _moverManager);
            var texture = _content.Load<Texture2D>("TopDownRpg/Path");
            var endTexture = _content.Load<Texture2D>("TopDownRpg/BluePathEnd");

            collisionSystem.AddCollisionSystem(new TiledCollisionSystem(_possibleMovements, Map, "Collision-Layer"));
            collisionSystem.AddCollisionSystem(_expiringSpatialHash);
            CollisionSystem = collisionSystem;
            AddClickController(PlayerEntity);
            PathRenderer = new PathRenderer(_moverManager, PlayerEntity, texture, endTexture, _tileSize.ToPoint());
            UpdateList.Add(_expiringSpatialHash);
            UpdateList.Add(entityController);
            UpdateList.Add(_spatialHashMover);
            UpdateList.Add(_moverManager);
            UpdateList.Add(new CameraTracker(Camera, EntityRenderersDict[PlayerEntity]));
            LoadEntities();
            var dialogFont = _content.Load<SpriteFont>("dialog");
            DialogBox = new EntityStoryBoxDialog(dialogFont);
            storyEngine.LoadWorld(AddEntity, RemoveEntity, worldName);
            UpdateList.Add(DialogBox);
            InteractEvent += (sender, args) =>
            {
                var facingDirection = PlayerEntity.FacingDirection;
                var interactTarget = (PlayerEntity.Position + facingDirection).ToPoint();
                Interact(interactTarget);
            };
            AddInteractionController();
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

        public void AddPlayer()
        {
            var entityRenderer = new PlayerEntityRenderer(_content, _expiringSpatialHash, PlayerEntity, _tileSize.ToPoint());
            _expiringSpatialHash.AddNode(PlayerEntity.Position.ToPoint(), PlayerEntity);
            RenderList.Add(entityRenderer);
            EntityRenderersDict[PlayerEntity] = entityRenderer;
            _spatialHashMover.Add(PlayerEntity);
        }

        public void AddEntity(Entity entity)
        {
            var entityRenderer = new EntityRenderer(_content, _expiringSpatialHash, entity, _tileSize.ToPoint());
            _expiringSpatialHash.AddNode(entity.Position.ToPoint(), entity);
            RenderList.Add(entityRenderer);
            EntityRenderersDict[entity] = entityRenderer;
            _spatialHashMover.Add(entity);
        }

        public void AddClickController(Entity entity)
        {
            ClickEvent = point =>
            {
                var endPoint = Camera.ScreenToWorld(point.X, point.Y).ToPoint();
                BeginMoveTo(entity, endPoint / _tileSize.ToPoint());
            };
        }

        public void BeginMoveTo(Entity entity, Point endPoint)
        {
            var mapContainsEndPoint = endPoint.X <= Map.Width && endPoint.Y <= Map.Height;
            if (mapContainsEndPoint)
            {
                var moveTo = endPoint;
                var collision = _expiringSpatialHash.CheckCollision(moveTo) || WaterCollision(endPoint);
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
                    MoveEntityTo(moveTo, entity, _tileSize.ToPoint(), _moverManager, collision, endPoint);
                }
            }
        }

        public void Interact(Point interactTarget)
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
                    var entityDialogBox = DialogBox as EntityStoryBoxDialog;
                    entityDialogBox?.StartStory(story, interactWith);
                }
                else if (GameFlags.GetVariable<bool>("acquire_rod"))
                {
                    if (WaterCollision(interactTarget))
                    {
                        var random = new Random();
                        var fishComplete = random.Next(3) == 0;
                        var scriptName = fishComplete ? "fish_success.ink" : "fish_fail.ink";
                        var fishScript = StoryImporter.ReadStory(scriptName);
                        var story = new GameFrameStory(fishScript);
                        if (fishComplete)
                        {
                            Flags.FishCount++;
                            story.ChoosePathString("dialog");
                            story.SetVariableState("fish_count", Flags.FishCount);
                        }
                        story.Continue();
                        DialogBox.StartStory(story);
                    }
                }
            }
        }

        public bool WaterCollision(Point p)
        {
            const string layerName = "Water-Layer";
            var waterLayer = Map.GetLayer<TiledTileLayer>(layerName);
            var collision = false;
            if (waterLayer != null)
            {
                var waterCollision = new TiledCollisionSystem(_possibleMovements, Map, layerName);
                collision = waterCollision.CheckCollision(p);
            }
            return collision;
        }

        public void MoveEntityTo(Point endPoint, Entity entity, Point tileSize, MoverManager moverManager, bool interact = false, Point? interactWith = null)
        {
            var searchParams = new SearchParameters(entity.Position.ToPoint(), endPoint, CollisionSystem, new Rectangle(new Point(), tileSize));
            var path = new AStarPathFinder(searchParams, _possibleMovements).FindPath();
            var pathMover = new PathMover(entity, new FinitePath(path), new ExpiringSpatialHashMovementComplete<Entity>(_expiringSpatialHash, PlayerEntity));
            pathMover.OnCancelEvent += (sender, args) => entity.MovingDirection = new Vector2();
            if (interact && interactWith != null)
            {
                pathMover.OnCompleteEvent += (sender, args) => Interact(interactWith.Value);
            }
            moverManager.AddMover(pathMover);
        }

        public override void Draw(SpriteBatch spriteBatch)
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
            DialogBox.Draw(spriteBatch);
        }

        public override void Dispose()
        {
            _content.Unload();
            _content.Dispose();
        }
    }
}
