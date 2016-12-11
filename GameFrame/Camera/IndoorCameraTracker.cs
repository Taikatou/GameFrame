using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using GameFrame.Common;

namespace GameFrame.Camera
{
    public class IndoorCameraTracker : AbstractCameraTracker
    {
        public IndoorCameraTracker(ViewportAdapter viewPort, IFocusAble following) : base(viewPort, following)
        {
            Camera.MaximumZoom = 8f;
            Camera.MinimumZoom = 0.5f;
        }

        public override void Zoom(float zoomBy)
        {
            CameraZoom += zoomBy;
            ReFocus();
        }

        public override void ReFocus()
        {
            var focus = GetFocus();
            Camera.Position = focus - Camera.Origin;
        }
    }
}
