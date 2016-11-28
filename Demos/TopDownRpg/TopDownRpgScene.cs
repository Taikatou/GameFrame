using Demos.TopDownRpg.Entities;
using Demos.TopDownRpg.Factory;
using Demos.TopDownRpg.GameModes;
using Demos.TopDownRpg.SpeedState;
using GameFrame;
using GameFrame.CollisionSystems.Tiled;
using GameFrame.Common;
using GameFrame.PathFinding.Heuristics;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.TopDownRpg
{
    public class TopDownRpgScene : DemoScreen
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ViewportAdapter _viewPort;
        private PossibleMovementWrapper _possibleMovements;
        public int BattleProbability { get; set; }
        public OpenWorldGameMode OpenWorldGameMode { get; set; }
        private readonly EntityManager _entityManager;
        private readonly StoryEngine _storyEngine;
        public TopDownRpgScene(ViewportAdapter viewPort, SpriteBatch spriteBatch) : base(viewPort, spriteBatch)
        {
            _viewPort = viewPort;
            _spriteBatch = spriteBatch;
            BattleProbability = 12;
            Move moveDelegate = (entity, point) => OpenWorldGameMode.BeginMoveTo(entity, point);
            _entityManager = new EntityManager(moveDelegate);
            var gameModeController = new GameModeController()
            {
                PushGameModeDelegate = mode => GameModes.Push(mode),
                PopGameModeDelegate = () => GameModes.Pop()
            };
            _storyEngine = new StoryEngine(gameModeController, moveDelegate, _entityManager);
        }

        public void LoadOpenWorld(string levelName)
        {
            _possibleMovements = new PossibleMovementWrapper(new EightWayPossibleMovement(new CrowDistance()));
            OpenWorldGameMode = new OpenWorldGameMode(_viewPort, _possibleMovements, PlayerEntity.Instance, levelName , new EntityControllerFactory(), _entityManager, _storyEngine);
            var map = OpenWorldGameMode.Map;
            var player = OpenWorldGameMode.PlayerEntity;
            var grassLayer = map.GetLayer<TiledTileLayer>("Grass-Layer");
            if (grassLayer != null)
            {
                var grassCollisionSystem = new TiledCollisionSystem(_possibleMovements, map, "Grass-Layer");
                OpenWorldGameMode.PlayerEntity.OnMoveEvent += (sender, args) =>
                {
                    var point = player.Position.ToPoint();
                    var grassCollision = grassCollisionSystem.CheckCollision(point);
                    if (grassCollision)
                    {
                        player.SpeedContext.Terrain = new SpeedGrass();
                    }
                    else if (player.SpeedContext.Terrain != null)
                    {
                        player.SpeedContext.Terrain = null;
                    }
                };
            }
            var teleportlayer = map.GetObjectGroup("Teleport-Layer");
            if (teleportlayer != null)
            {
                var tileSize = new Point(map.TileWidth, map.TileHeight);
                var teleporters = new TiledObjectCollisionSystem(_possibleMovements, map, tileSize, "Teleport-Layer");
                OpenWorldGameMode.PlayerEntity.OnMoveEvent += (sender, args) =>
                {
                    var point = player.Position.ToPoint();
                    if (teleporters.CheckCollision(point))
                    {
                        var teleporter = teleporters.GetObjectAt(point);
                        var position = StringToVector.ConvertString(teleporter.Type);
                        PlayerEntity.Instance = new PlayerEntity(position);
                        GameModeStack.Unload();
                        LoadOpenWorld(teleporter.Name);
                    }
                };
            }
            GameModes.Push(OpenWorldGameMode);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            PlayerEntity.Instance = new PlayerEntity
            {
                Name ="Player",
                SpriteSheet = "Character",
                Position = new Vector2(15,15)
            };
            Flags.PrincessKidnapped = true;
            LoadOpenWorld("north_desert_hideout_second_floor");
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsVisible)
            {
                CurrentGameMode.Draw(_spriteBatch);
            }
            base.Draw(gameTime);
        }
    }
}
