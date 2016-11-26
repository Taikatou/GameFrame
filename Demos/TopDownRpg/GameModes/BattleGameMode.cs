using System.Collections.Generic;
using GameFrame;
using GameFrame.Content;
using GameFrame.Ink;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Demos.TopDownRpg.GameModes
{
    public class BattleGameMode : IGameMode
    {
        private Entity _battleWith;
        public List<IUpdate> UpdateList;
        public List<IRenderable> RenderList;
        private readonly ContentManager _content;
        private readonly BattleStoryBoxDialog _dialogBox;
        private Vector2 _centerPoint;
        private Rectangle _rectangle;
        public BattleGameMode(Entity battleWith, string battleScriptName, GameModeController gameModeController)
        {
            _battleWith = battleWith;
            UpdateList = new List<IUpdate>();
            RenderList = new List<IRenderable>();
            _content = ContentManagerFactory.RequestContentManager();
            LoadEntities(battleWith);
            var dialogFont = _content.Load<SpriteFont>("dialog");
            _dialogBox = new BattleStoryBoxDialog(dialogFont)
            {
                CompleteEvent = (sender, args) => gameModeController.PopGameMode()
            };
            var storyFile = StoryImporter.ReadStory(battleScriptName);
            var story = new GameFrameStory(storyFile);
            story.Continue();
            var graphicsDevice = StaticServiceLocator.GetService<GraphicsDevice>();
            _centerPoint = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 2f);
            story.ObserveVariable("progress", (varName, newValue) => {

            });
            _dialogBox.StartStory(story);
            UpdateList.Add(_dialogBox);
        }

        public void LoadEntities(Entity entity)
        {
            _frameRectangle = new Rectangle(new Point(), new Point(16, 16));
            var graphicsDevice = StaticServiceLocator.GetService<GraphicsDevice>();
            var posX = _centerPoint.X - (frameRectangle.X / 2f);
            var posY = graphicsDevice.Viewport.Height - frameRectangle.Y - 30;
            var position = new Vector2(posX, posY);
            var enemyRenderer = new BattleEntityRenderer(position, frameRectangle, entity, _content);
            RenderList.Add(enemyRenderer);
        }

        public int PosX
        {
            get
            {
                return _centerPoint.X - (_frameRectangle.X / 2f);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var toRender in RenderList)
            {
                toRender.Draw(spriteBatch);
            }
            spriteBatch.End();
            _dialogBox.Draw(spriteBatch);
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
