using GameFrame.Camera;
using GameFrame.Common;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace Demos.TopDownRpg.Factory
{
    public class CameraTrackerFactory
    {
        public static AbstractCameraTracker CreateTracker(ViewportAdapter viewPort, IFocusAble following, TiledMap map)
        {
            AbstractCameraTracker tracker;
            if (map.Width < 20)
            {
                tracker = new IndoorCameraTracker(viewPort, following);
            }
            else
            {
                tracker = new OutdoorCameraTracker(viewPort, following, map);
            }
            return tracker;
        }
    }
}
