using System.Collections.Generic;
using GameFrame.Controllers.SmartButton;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Controllers
{
    public class SmartController : IUpdate
    {
        public List<AbstractSmartButton> Buttons;
        public SmartController()
        {
            Buttons = new List<AbstractSmartButton>();
        }

        public void AddButton(AbstractSmartButton button)
        {
            Buttons.Add(button);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var button in Buttons)
            {
                button.Update();
            }
        }
    }
}
