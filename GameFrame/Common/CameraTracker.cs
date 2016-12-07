using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace GameFrame.Common
{
    public class CameraTracker : IUpdate
    {
        public Camera2D Camera;
        private readonly IFocusAble _following;
        private Vector2 _cachedPosition;
        private static float _cameraZoom = 2.0f;
        public float CameraZoom
        {
            get { return _cameraZoom; }
            set
            {
                if (value >= Camera.MinimumZoom && value <= Camera.MaximumZoom)
                {
                    _cameraZoom = value;
                    Camera.Zoom = _cameraZoom;
                }
            }
        }

        public Matrix TransformationMatrix => Camera.GetViewMatrix();

        public CameraTracker(ViewportAdapter viewPort, IFocusAble following)
        {
            Camera = new Camera2D(viewPort) { Zoom = CameraZoom };
            _following = following;
        }

        public void ReFocus()
        {
            var focusOn = _following.Position + _following.Offset;
            Camera.LookAt(focusOn);
            _cachedPosition = _following.Position;
        }

        public void Update(GameTime gameTime)
        {
            if (_cachedPosition != _following.Position)
            {
                ReFocus();
            }
        }

        public Vector2 ScreenToWorld(int x, int y)
        {
            return Camera.ScreenToWorld(x, y);
        }

        public Vector2 ScreenToWorld(Point position)
        {
            return Camera.ScreenToWorld(position.X, position.Y);
        }

        public bool Contains(Rectangle rectangle)
        {
            var contains = Camera.Contains(rectangle);
            return contains != ContainmentType.Disjoint;
        }

        public void Zoom(float zoomBy)
        {
            CameraZoom += zoomBy;
        }
    }
}
