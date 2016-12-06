using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using MonoGame.Extended;

namespace GameFrame.Controllers.Click.TouchScreen
{
    public class TouchScreenControl : IUpdate
    {
        private readonly Dictionary<GestureType, SmartGesture> _gestureEvents;
        public TouchScreenControl()
        {
            _gestureEvents = new Dictionary<GestureType, SmartGesture>();
        }

        public void AddSmartGesture(SmartGesture smartGesture)
        {
            TouchPanel.EnabledGestures |= smartGesture.Gesture;
            _gestureEvents[smartGesture.Gesture] = smartGesture;
        }

        public void Update(GameTime gameTime)
        {
            while (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                if (_gestureEvents.ContainsKey(gesture.GestureType))
                {
                    _gestureEvents[gesture.GestureType].Trigger(gesture);
                }
            }
        }
    }
}
