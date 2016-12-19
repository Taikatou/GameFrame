using GameFrame.Common;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace GameFrame.Camera
{
    public class OutdoorCameraTracker : AbstractCameraTracker
    {
        private readonly TiledMap _map;
        public float UMin => (_viewport.VirtualHeight / 2.0f) / CameraZoom;
        public float RMin => (_viewport.VirtualWidth / 2.0f) / CameraZoom;
        public float UMax => _map.HeightInPixels - UMin;
        public float RMax => _map.WidthInPixels - RMin;
        private readonly ViewportAdapter _viewport;
        public OutdoorCameraTracker(ViewportAdapter viewPort, IFocusAble following, TiledMap map) : base(viewPort, following)
        {
            _map = map;
            _viewport = viewPort;
            if (!ZoomToBig(CameraZoom))
            {
                CameraZoom = 2.0f;
            }
            Camera.MinimumZoom = 0.5f;
        }

        public bool ZoomToBig(float zoom)
        {
            var mapSize = new Size((int) (_map.WidthInPixels*zoom), (int) (_map.HeightInPixels*zoom));
            var maxScreenSize = new Size(_viewport.VirtualWidth, _viewport.VirtualHeight);
            var zoomToBig = mapSize.Width > maxScreenSize.Width && mapSize.Height > maxScreenSize.Height;
            return zoomToBig;
        }

        public override void Zoom(float zoomBy)
        {
            var newCamera = CameraZoom + zoomBy;
            if (newCamera > CameraZoom || ZoomToBig(newCamera))
            {
                CameraZoom = newCamera;
                ReFocus();
            }
        }

        public override void ReFocus()
        {
            var focus = GetFocus();
            if (focus.X < RMin)
            {
                focus.X = RMin;
            }
            else if(focus.X > RMax)
            {
                focus.X = RMax;
            }
            if (focus.Y < UMin)
            {
                focus.Y = UMin;
            }
            else if (focus.Y > UMax)
            {
                focus.Y = UMax;
            }
            Camera.Position = focus - Camera.Origin;
        }
    }
}
