using System;
using Demos.TopDownRpg.Factory;
using Demos.TopDownRpg.GameModes;
using Demos.TopDownRpg.SpeedState;
using GameFrame.CollisionSystems.Tiled;
using GameFrame.Common;
using GameFrame.Controllers;
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
        public Entity PlayerEntity;
        private OpenWorldGameMode _openWorldGameMode;
        public TopDownRpgScene(ViewportAdapter viewPort, SpriteBatch spriteBatch) : base(viewPort, spriteBatch)
        {
            _viewPort = viewPort;
            _spriteBatch = spriteBatch;
            BattleProbability = 12;
        }

        public void LoadOpenWorld(string levelName)
        {
            _possibleMovements = new PossibleMovementWrapper(new EightWayPossibleMovement(new CrowDistance()));
            _openWorldGameMode = new OpenWorldGameMode(_viewPort, _possibleMovements, PlayerEntity, levelName , new TwoDEntityRenderer(), new EntityControllerFactory());
            var map = _openWorldGameMode.Map;
            var player = _openWorldGameMode.PlayerEntity;
            var grassLayer = map.GetLayer<TiledTileLayer>("Grass-Layer");
            if (grassLayer != null)
            {
                var grassCollisionSystem = new TiledCollisionSystem(_possibleMovements, map, "Grass-Layer");
                _openWorldGameMode.PlayerEntity.OnMoveEvent += (sender, args) =>
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
                            GameModes.Push(new BattleGameMode());
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
                _openWorldGameMode.PlayerEntity.OnMoveEvent += (sender, args) =>
                {
                    var point = player.Position.ToPoint();
                    if (teleporters.CheckCollision(point))
                    {
                        var teleporter = teleporters.GetObjectAt(point);
                        var position = StringToVector.ConvertString(teleporter.Type);
                        PlayerEntity = new Entity(PlayerEntity, position);
                        GameModeStack.Unload();
                        LoadOpenWorld(teleporter.Name);
                    }
                };
            }
            GameModes.Push(_openWorldGameMode);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            PlayerEntity = new Entity("Player", "Character", "") { Position = new Vector2(5, 5) };
            LoadOpenWorld("level01");
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
