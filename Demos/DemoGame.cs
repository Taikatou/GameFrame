using System.Collections.Generic;
using Demos.Common;
using Demos.Pong;
using Demos.Puzzle;
using Demos.Screens;
using Demos.TopDownRpg;
using GameFrame;
using GameFrame.Ink;
using GameFrame.Interceptor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.ViewportAdapters;

namespace Demos
{
    public class DemoGame : GameFrameGame
    {
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private readonly ScreenComponent _screenComponent;
        private readonly List<IInterceptor<StoryContext>> _storyInterceptors;
        private TopDownRpgScene _rpgScene;

        public void AddStoryInterceptor(IInterceptor<StoryContext> interceptor)
        {
            if (_rpgScene == null)
            {
                _storyInterceptors.Add(interceptor);
            }
            else
            {
                _rpgScene.AddStoryInterceptor(interceptor);
            }
        }

        public DemoGame()
        {
            _storyInterceptors = new List<IInterceptor<StoryContext>>();
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Components.Add(_screenComponent = new ScreenComponent(this));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, ScreenSize.Width, ScreenSize.Height);
            _rpgScene = new TopDownRpgScene(viewportAdapter, _spriteBatch);
            var screens = new List<Screen>
            {
                new MainMenuScreen(viewportAdapter, Services, this),
                new LoadGameScreen(viewportAdapter, Services),
                new OptionsScreen(viewportAdapter, Services),
                new AudioOptionsScreen(viewportAdapter, Services),
                new VideoOptionsScreen(viewportAdapter, Services),
                new KeyboardOptionsScreen(viewportAdapter, Services),
                new MouseOptionsScreen(viewportAdapter, Services),
                new PongScreen(viewportAdapter, _spriteBatch),
                _rpgScene
            };
            foreach (var screen in screens)
            {
                screen.LoadContent();
                _screenComponent.Register(screen);
            }
            foreach (var interceptor in _storyInterceptors)
            {
                _rpgScene.AddStoryInterceptor(interceptor);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
