using Microsoft.Xna.Framework.Input.Touch;

namespace GameFrame.Controllers.Click.TouchScreen
{
    public delegate void GestureEvent(GestureSample gesture);
    public class SmartGesture
    {
        public readonly GestureType Gesture;
        public GestureEvent GestureEvent;
        public SmartGesture(GestureType gesture)
        {
            Gesture = gesture;
        }

        public void Trigger(GestureSample gesture)
        {
            GestureEvent?.Invoke(gesture);
        }
    }
}
