using GameFrame.Controllers.Click.MouseClick;
using GameFrame.Controllers.Click.TouchScreen;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.Controllers.Click
{
    public class ClickController : IUpdate, ITouchScreenControl
    {
        public MouseControl MouseControl;
        public TouchScreenControl TouchScreenControl;

        public ClickController()
        {
            MouseControl = new MouseControl();
            TouchScreenControl = new TouchScreenControl();
        }

        public ClickController(MouseControl mouseControl, TouchScreenControl touchScreenControl)
        {
            MouseControl = mouseControl;
            TouchScreenControl = touchScreenControl;
        }

        public void Update(GameTime gameTime)
        {
            MouseControl.Update(gameTime);
            TouchScreenControl.Update(gameTime);
        }

        public void AddSmartGesture(SmartGesture smartGesture)
        {
            TouchScreenControl.AddSmartGesture(smartGesture);
        }
    }
}
