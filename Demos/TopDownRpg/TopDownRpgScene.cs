using System;
using Demos.TopDownRpg.Factory;
using Demos.TopDownRpg.GameModes;
using GameFrame;
using GameFrame.CollisionSystems.Tiled;
using GameFrame.Common;
using GameFrame.PathFinding.Heuristics;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.TopDownRpg
{
    public class TopDownRpgScene : GameFrameScreen
    {
        private readonly SpriteBatch _spriteBatch;
        private readonly ViewportAdapter _viewPort;
        public int BattleProbability { get; set; }
        public Entity PlayerEntity;
        public TopDownRpgScene(ViewportAdapter viewPort, SpriteBatch spriteBatch)
        {
            _viewPort = viewPort;
            _spriteBatch = spriteBatch;
            BattleProbability = 12;
        }

        public void LoadOpenWorld(string levelName)
        {
            AbstractEntityFactory factory = new EntityFactoryA();
            var possibleMovements = new FourWayPossibleMovement();
            var openWorldGameMode = new OpenWorldGameMode(_viewPort, possibleMovements, PlayerEntity, levelName , factory);
            var map = openWorldGameMode.Map;
            var grassCollisionSystem = new TiledCollisionSystem(possibleMovements, map, "Grass-Layer");
            var player = openWorldGameMode.PlayerEntity;
            var tileSize = new Point(map.TileWidth, map.TileHeight);
            var teleporters = new TiledObjectCollisionSystem(possibleMovements, map, tileSize, "Teleport-Layer");
            openWorldGameMode.PlayerEntity.OnMoveCompleteEvent += (sender, args) =>
            {
                var random = new Random();
                var point = player.Position.ToPoint();
                var grassCollision = grassCollisionSystem.CheckCollision(point);
                var grassProbability = random.Next(BattleProbability);
                if (grassCollision && grassProbability == 0)
                {
                    GameModeStack.Push(new BattleGameMode());
                }

                if (teleporters.CheckCollision(point))
                {
                    var teleporter = teleporters.GetObjectAt(point);
                    var position = StringToVector.ConvertString(teleporter.Type);
                    PlayerEntity.Position = position;
                    var oldWorld = GameModeStack.Pop();
                    oldWorld.Dispose();
                    LoadOpenWorld(teleporter.Name);
                }
            };
            GameModeStack.Push(openWorldGameMode);
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
                GameModeStack.Peek().Draw(_spriteBatch);
            }
        }
    }
}
