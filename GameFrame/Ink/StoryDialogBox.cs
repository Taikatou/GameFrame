using System.Collections.Generic;
using GameFrame.Movers;
using GameFrame.Renderers;
using GameFrame.ServiceLocator;
using Ink.Runtime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameFrame.Ink
{
    public class StoryDialogBox : IUpdate
    {
        private readonly DialogBox _dialogBox;
        private readonly BaseMovable _player;
        private Vector2 _cachedPosition;
        public readonly StoryDispatcher StoryDispatcher;
        private readonly Camera2D _camera;

        public StoryDialogBox(SpriteFont font, BaseMovable player)
        {
            _dialogBox = new DialogBox(font);
            _dialogBox.Initialize();
            _dialogBox.Hide();
            _player = player;
            StoryDispatcher = new StoryDispatcher();
            if (StaticServiceLocator.ContainsService<List<StoryInterceptor>>())
            {
                var interceptors = StaticServiceLocator.GetService<List<StoryInterceptor>>();
                foreach (var interceptor in interceptors)
                {
                    StoryDispatcher.AddInterceptor(interceptor);
                }
            }
            var graphicsDevice = StaticServiceLocator.GetService<GraphicsDevice>();
            _camera = new Camera2D(graphicsDevice) { Zoom = 1.0f };
        }

        public void Update(GameTime gameTime)
        {
            if (_dialogBox.Active)
            {
                if (_cachedPosition != _player.Position)
                {
                    _dialogBox.Hide();
                }
                else
                {
                    _dialogBox.Update(gameTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_dialogBox.Active)
            {
                var transformMatrix = _camera.GetViewMatrix();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, transformMatrix: transformMatrix);
                _dialogBox.Draw(spriteBatch);
                spriteBatch.End();
            }
        }

        public void AddDialogBox(Story story)
        {
            var text = story.ContinueMaximally();
            _dialogBox.Text = text;
            _dialogBox.Show();
            _cachedPosition = _player.Position;
            StoryDispatcher.AddStory(story);
        }
    }
}
