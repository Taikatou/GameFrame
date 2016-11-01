﻿using System;
using Demos.TopDownRpg.Factory;
using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.CollisionSystems.Tiled;
using GameFrame.Common;
using GameFrame.Controllers;
using GameFrame.PathFinding.Heuristics;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public TopDownRpgScene(ViewportAdapter viewPort, SpriteBatch spriteBatch) : base(viewPort, spriteBatch)
        {
            _viewPort = viewPort;
            _spriteBatch = spriteBatch;
            BattleProbability = 12;
        }

        public void LoadOpenWorld(string levelName)
        {
            ControllerFactory controllerFactory = new EntityControllerFactory();
            RendererFactory rendererFactory = new TwoDEntityRenderer();

            _possibleMovements = new PossibleMovementWrapper(new EightWayPossibleMovement(new CrowDistance()));
            var openWorldGameMode = new OpenWorldGameMode(_viewPort, _possibleMovements, PlayerEntity, levelName , rendererFactory, controllerFactory);
            var map = openWorldGameMode.Map;
            var grassCollisionSystem = new TiledCollisionSystem(_possibleMovements, map, "Grass-Layer");
            var player = openWorldGameMode.PlayerEntity;
            var tileSize = new Point(map.TileWidth, map.TileHeight);
            var teleporters = new TiledObjectCollisionSystem(_possibleMovements, map, tileSize, "Teleport-Layer");
            openWorldGameMode.PlayerEntity.OnMoveCompleteEvent += (sender, args) =>
            {
                var random = new Random();
                var point = player.Position.ToPoint();
                var grassCollision = grassCollisionSystem.CheckCollision(point);
                var grassProbability = random.Next(BattleProbability);
                if (grassCollision && grassProbability == 0)
                {
                    GameModes.Push(new BattleGameMode());
                }

                if (teleporters.CheckCollision(point))
                {
                    var teleporter = teleporters.GetObjectAt(point);
                    PlayerEntity.Position = StringToVector.ConvertString(teleporter.Type);
                    GameModeStack.Unload();
                    LoadOpenWorld(teleporter.Name);
                }
            };
            GameModes.Push(openWorldGameMode);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            PlayerEntity = new Entity(new Vector2(5, 5));
            LoadOpenWorld("level01");
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsVisible)
            {
                CurrentGameMode.Draw(_spriteBatch);
                base.Draw(gameTime);
            }
        }
    }
}
