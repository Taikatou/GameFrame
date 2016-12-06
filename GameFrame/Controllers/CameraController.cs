using GameFrame.Common;
using GameFrame.Controllers.Click;
using GameFrame.Controllers.Click.TouchScreen;
using GameFrame.ServiceLocator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace GameFrame.Controllers
{
    public class CameraController
    {
        public static bool TouchScreenEnabled
        {
            get
            {
                var controllerSettings = StaticServiceLocator.GetService<IControllerSettings>();
                return controllerSettings.TouchScreenEnabled;
            }
        }

        public static bool KeyboardMouseEnabled
        {
            get
            {
                var controllerSettings = StaticServiceLocator.GetService<IControllerSettings>();
                return controllerSettings.KeyBoardMouseEnabled;
            }
        }

        public static void AddCameraZoomController(CameraTracker camera, ClickController clickController)
        {
            if (TouchScreenEnabled)
            {
                var pinchGesture = new SmartGesture(GestureType.Pinch)
                {
                    GestureEvent = gesture =>
                    {
                        var dist = Vector2.Distance(gesture.Position, gesture.Position2);

                        var aOld = gesture.Position - gesture.Delta;
                        var bOld = gesture.Position2 - gesture.Delta2;
                        var distOld = Vector2.Distance(aOld, bOld);

                        var scale = (distOld - dist)/500f;
                        camera.Zoom(scale);
                    }
                };
                clickController.TouchScreenControl.AddSmartGesture(pinchGesture);
            }
            if (KeyboardMouseEnabled)
            {
                clickController.MouseControl.OnScrollEvent += zoomBy =>
                {
                    camera.Zoom((float)zoomBy / 1000);
                };
            }
        }

        public static void AddCameraMovementController(CameraTracker camera, ClickController clickController)
        {
            if (TouchScreenEnabled)
            {
                var dragGesture = new SmartGesture(GestureType.FreeDrag)
                {
                    GestureEvent = gesture =>
                    {
                        camera.Camera.Position -= gesture.Delta/camera.CameraZoom;
                    }
                };
                clickController.TouchScreenControl.AddSmartGesture(dragGesture);
            }
        }
    }
}
