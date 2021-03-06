﻿using System;
using Demos.Screens;
using Demos.TopDownRpg.Entities;
using Demos.TopDownRpg.GameModes;
using Demos.TopDownRpg.SpeedState;
using GameFrame;
using GameFrame.CollisionSystems.Tiled;
using GameFrame.Common;
using GameFrame.PathFinding.PossibleMovements;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.TopDownRpg
{
    public delegate void Teleport(string worldName);
    public class TopDownRpgScene : GameFrameScreen
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ViewportAdapter _viewPort;
        private PossibleMovementWrapper _possibleMovements;
        public int BattleProbability { get; set; }
        public OpenWorldGameMode OpenWorldGameMode { get; set; }
        private readonly EntityManager _entityManager;
        private readonly StoryEngine _storyEngine;
        public static int Speed = 4;
        public TopDownRpgScene(ViewportAdapter viewPort, SpriteBatch spriteBatch)
        {
            _viewPort = viewPort;
            _spriteBatch = spriteBatch;
            BattleProbability = 12;
            Move moveDelegate = (entity, point) => OpenWorldGameMode.BeginMoveTo(entity, point);
            _entityManager = new EntityManager(moveDelegate);
            var gameModeController = new GameModeController
            {
                PushGameModeDelegate = mode => GameModes.Push(mode),
                PopGameModeDelegate = () => GameModes.Pop()
            };
            Say say = story => OpenWorldGameMode.DialogBox.StartStory(story);
            _storyEngine = new StoryEngine(gameModeController, moveDelegate, LoadOpenWorld, say);
        }

        public static EventHandler ClickEvent;
        public void LoadOpenWorld(string levelName)
        {
            var possibleMovements = StaticServiceLocator.GetService<IPossibleMovements>();
            _possibleMovements = new PossibleMovementWrapper(possibleMovements);
            ClickEvent = (sender, args) =>
            {
                Action action = Show<MainMenuScreen>;
                action.Invoke();
            };
            OpenWorldGameMode = new OpenWorldGameMode(_viewPort, _possibleMovements, levelName, _entityManager, _storyEngine, ClickEvent);
            var map = OpenWorldGameMode.Map;
            var player = PlayerEntity.Instance;
            var grassLayer = map.GetLayer<TiledTileLayer>("Grass-Layer");
            if (grassLayer != null)
            {
                var grassCollisionSystem = new TiledCollisionSystem(_possibleMovements, map, "Grass-Layer");
                PlayerEntity.Instance.OnMoveEvent += (sender, args) =>
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
                PlayerEntity.Instance.OnMoveEvent += (sender, args) =>
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
                Position = new Vector2(5, 5)
            };
            LoadOpenWorld("player_home");
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
