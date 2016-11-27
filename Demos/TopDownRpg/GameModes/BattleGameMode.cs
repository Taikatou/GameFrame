using System;
using System.Collections.Generic;
using Demos.TopDownRpg.Entities;
using GameFrame.Content;
using GameFrame.Ink;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Demos.TopDownRpg.GameModes
{
    public class BattleGameMode : AbstractRpgGameMode
    {
        public List<BattleEntityRenderer> EntityList;
        private readonly ContentManager _content;
        private Vector2 _centerPoint;
        public EventHandler CompleteEvent { get; set; }
        public BattleGameMode(Entity battleWith)
        {
            _content = ContentManagerFactory.RequestContentManager();
            LoadEntities(battleWith);
            var dialogFont = _content.Load<SpriteFont>("dialog");
            DialogBox = new BattleStoryBoxDialog(dialogFont)
            {
                CompleteEvent = (sender, args) => Complete()
            };
            var graphicsDevice = StaticServiceLocator.GetService<GraphicsDevice>();
            _centerPoint = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 2f);
            UpdateList.Add(DialogBox);
            var rectangle = new Rectangle(new Point(), new Point(16,16));
            var enemyEntity = new BattleEntityRenderer(rectangle, battleWith, _content);
            var playerEntity = new BattleEntityRenderer(rectangle, PlayerEntity.Instance, _content);
            EntityList = new List<BattleEntityRenderer> { enemyEntity, playerEntity };
            AddInteractionController();
        }

        public void StartStory(string battleScriptName)
        {
            var storyFile = StoryImporter.ReadStory(battleScriptName);
            var story = new GameFrameStory(storyFile);
            story.Continue();
            story.ObserveVariable("progress", (varName, newValue) =>
            {
                var value = (int)newValue;
                _centerPoint.Y = value;
                if (value <= 0.0f || value >= 1.0f)
                {
                    Complete();
                }
            });
            DialogBox.StartStory(story);
        }

        public void LoadEntities(Entity entity)
        {
            
        }

        public void Complete()
        {
            CompleteEvent?.Invoke(this, null);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var entity in EntityList)
            {
                entity.Draw(spriteBatch, _centerPoint);
            }
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
