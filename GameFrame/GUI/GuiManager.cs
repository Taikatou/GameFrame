using System.Collections.Generic;
using System.Linq;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace GameFrame.GUI
{
    public class GuiManager : IGuiLayer
    {
        private readonly Camera2D _camera;
        private readonly List<IGuiLayer> _guiLayers;

        public GuiManager()
        {
            _guiLayers = new List<IGuiLayer>();
            var graphicsDevice = StaticServiceLocator.GetService<BoxingViewportAdapter>();
            _camera = new Camera2D(graphicsDevice) { Zoom = 1.0f };
        }

        public void AddGuiLayer(IGuiLayer layer)
        {
            _guiLayers.Add(layer);
        }
        public void Update(GameTime gameTime)
        {
            foreach (var layer in _guiLayers)
            {
                layer.Update(gameTime);
            }
        }

        public bool Interact(Point p)
        {
            var point = _camera.ScreenToWorld(p.ToVector2()).ToPoint();
            return _guiLayers.Any(layer => layer.Interact(point));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var transformMatrix = _camera.GetViewMatrix();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, transformMatrix: transformMatrix);
            foreach (var layer in _guiLayers)
            {
                layer.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
