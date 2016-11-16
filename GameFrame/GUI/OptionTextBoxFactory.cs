using System.Collections.Generic;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.GUI
{
    public class OptionTextBoxFactory
    {
        public static void LineTextBoxes(List<OptionTextBox> options)
        {
            var graphicsDevice = StaticServiceLocator.GetService<GraphicsDevice>();
            var centerScreen = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 2f);
            for(var i = 0; i < options.Count; i++)
            {
                var option = options[i];
                var posX = centerScreen.X - option.Size.X / 2f;
                var posY = (option.Size.Y + 30)*(i + 1);
                option.Position = new Vector2(posX, posY);
            }
        }
    }
}
