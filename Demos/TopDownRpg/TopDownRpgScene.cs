using System;
using Demos.TopDownRpg.Entities;
using Demos.TopDownRpg.Factory;
using Demos.TopDownRpg.GameModes;
using Demos.TopDownRpg.SpeedState;
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
        public KeyboardUpdater KeyBoardUpdater;
        private readonly EntityManager _entityManager;
        private readonly StoryEngine _storyEngine;
        public TopDownRpgScene(ViewportAdapter viewPort, SpriteBatch spriteBatch) : base(viewPort, spriteBatch)
        {
            _viewPort = viewPort;
            _spriteBatch = spriteBatch;
            BattleProbability = 12;
            KeyBoardUpdater = new KeyboardUpdater();
            Move moveDelegate = (entity, point) => OpenWorldGameMode.Move(entity, point);
            _entityManager = new EntityManager(moveDelegate);
            _storyEngine = new StoryEngine(moveDelegate, _entityManager);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            KeyBoardUpdater.Update(gameTime);
        }

        public void LoadOpenWorld(string levelName)
        {
            _possibleMovements = new PossibleMovementWrapper(new EightWayPossibleMovement(new CrowDistance()));
            OpenWorldGameMode = new OpenWorldGameMode(_viewPort, _possibleMovements, PlayerEntity.Instance, levelName , new EntityControllerFactory(KeyBoardUpdater), _entityManager, _storyEngine);
            var map = OpenWorldGameMode.Map;
            var player = OpenWorldGameMode.PlayerEntity;
            var grassLayer = map.GetLayer<TiledTileLayer>("Grass-Layer");
            if (grassLayer != null)
            {
                var grassCollisionSystem = new TiledCollisionSystem(_possibleMovements, map, "Grass-Layer");
                OpenWorldGameMode.PlayerEntity.OnMoveEvent += (sender, args) =>
                {
                    var random = new Random();
                    var point = player.Position.ToPoint();
                    var grassCollision = grassCollisionSystem.CheckCollision(point);
                    var grassProbability = random.Next(BattleProbability);
                    if (grassCollision)
                    {
                        player.SpeedContext.Terrain = new SpeedGrass();
                        if (grassProbability == 0)
                        {
                            // GameModes.Push(new BattleGameMode());
                        }
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
                Position = new Vector2(92, 84)
            };
            LoadOpenWorld("west_forest");
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
