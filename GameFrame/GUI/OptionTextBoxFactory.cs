using System.Collections.Generic;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameFrame.GUI
{
    public class OptionTextBoxFactory
    {
        public static void LineTextBoxes(List<OptionTextBox> options, Size size)
        {
            var centerScreen = new Vector2(size.Width / 2f, size.Height / 2f);
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
