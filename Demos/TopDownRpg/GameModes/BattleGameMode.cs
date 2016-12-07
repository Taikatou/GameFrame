using System.Collections.Generic;
using Demos.Common;
using Demos.TopDownRpg.Entities;
using GameFrame.Content;
using GameFrame.Controllers;
using GameFrame.Ink;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.TopDownRpg.GameModes
{
    public delegate void CompleteEvent(bool win);
    public class BattleGameMode : AbstractRpgGameMode
    {
        public List<BattleEntityRenderer> EntityList;
        private readonly ContentManager _content;
        public CompleteEvent CompleteEvent { get; set; }
        public readonly Camera2D Camera;
        private readonly Texture2D _backgroundTexture;
        private readonly Rectangle _destinationRectangle;
        private GameFrameStory _activeStory;

        public BattleGameMode(Entity battleWith)
        {
            var viewPort = StaticServiceLocator.GetService<BoxingViewportAdapter>();
            Camera = new Camera2D(viewPort) { Zoom = 1.0f };
            _content = ContentManagerFactory.RequestContentManager();
            _backgroundTexture = _content.Load<Texture2D>($"TopDownRpg/hills");
            var dialogFont = _content.Load<SpriteFont>("dialog");
            var settings = StaticServiceLocator.GetService<IControllerSettings>();
            DialogBox = new BattleStoryBoxDialog(ScreenSize.Size, dialogFont, settings.GamePadEnabled)
            {
                CompleteEvent = (sender, args) => Complete()
            };
            GuiManager.AddGuiLayer(DialogBox);
            var rectangle = new Rectangle(new Point(), new Point(16,16));
            var enemyEntity = new BattleEntityRenderer(new Rectangle(new Point(600, 300), new Point(160, 160)), rectangle, battleWith, _content);
            var playerEntity = new BattleEntityRenderer(new Rectangle(new Point(50, 300), new Point(160, 160)), rectangle, PlayerEntity.Instance, _content);
            EntityList = new List<BattleEntityRenderer> { enemyEntity, playerEntity };
            AddInteractionController();
            var boundingRectangle = Camera.GetBoundingRectangle();
            var size = new Point((int) boundingRectangle.Size.Width, (int) boundingRectangle.Size.Height);
            _destinationRectangle = new Rectangle(boundingRectangle.Location.ToPoint(), size);
        }

        public void StartStory(string battleScriptName)
        {
            var storyFile = StoryImporter.ReadStory(battleScriptName);
            var story = new GameFrameStory(storyFile);
            story.Continue();
            DialogBox.StartStory(story);
            _activeStory = story;
        }

        public void Complete()
        {
            var victory = _activeStory.GetVariableState<int>("victory") == 1;
            CompleteEvent?.Invoke(victory);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var transformMatrix = Camera.GetViewMatrix();
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix,
                              sortMode: SpriteSortMode.BackToFront, depthStencilState: DepthStencilState.Default);
            spriteBatch.Draw(_backgroundTexture, _destinationRectangle, Color.White);
            foreach (var entity in EntityList)
            {
                entity.Draw(spriteBatch);
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
