using GameFrame.CameraTracker;
using GameFrame.Common;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.TopDownRpg
{
    public class CameraFactory
    {
        public static AbstractCameraTracker CreateCamera(ViewportAdapter viewPort, IFocusAble following, TiledMap map)
        {
            AbstractCameraTracker camera;
            var width = map.Width;
            if (width < 30)
            {
                camera = new IndoorCameraTracker(viewPort, following);
            }
            else
            {
                camera = new OutdoorCameraTracker(viewPort, following);
            }
            return camera;
        }
    }
}
