using GameFrame.Common;
using MonoGame.Extended.ViewportAdapters;

namespace GameFrame.CameraTracker
{
    public class IndoorCameraTracker : AbstractCameraTracker
    {
        public IndoorCameraTracker(ViewportAdapter viewPort, IFocusAble following) : base(viewPort, following)
        {
        }

        public override void ReFocus()
        {
            var focusOn = Following.Position + Following.Offset;
            Camera.LookAt(focusOn);
            CachedPosition = Following.Position;
        }
    }
}
