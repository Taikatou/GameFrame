using System.Diagnostics;
using GameFrame.Common;
using Microsoft.Xna.Framework;
using MonoGame.Extended.ViewportAdapters;

namespace GameFrame.CameraTracker
{
    public class OutdoorCameraTracker : AbstractCameraTracker
    {
        private float _width;
        private float _height;
        public OutdoorCameraTracker(ViewportAdapter viewPort, IFocusAble following) : base(viewPort, following)
        {
            _width = viewPort.VirtualWidth;
            _height = viewPort.VirtualHeight;
        }

        public override void ReFocus()
        {
            var focusOn = Following.Position + Following.Offset;
            var lookAt = focusOn - Camera.Origin;
            Position = lookAt;
        }
    }
}
