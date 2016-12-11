using GameFrame.Common;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace GameFrame.Camera
{
    public abstract class AbstractCameraTracker : IUpdate
    {
        public Camera2D Camera;
        public readonly IFocusAble Following;
        public Vector2 CachedPosition;
        private static float _cameraZoom = 2.0f;
        public Matrix TransformationMatrix => Camera.GetViewMatrix();

        public Vector2 GetFocus()
        {
            return Following.Position + Following.Offset;
        }
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
        public AbstractCameraTracker(ViewportAdapter viewPort, IFocusAble following)
        {
            Camera = new Camera2D(viewPort) { Zoom = CameraZoom };
            Following = following;
        }
        public void Update(GameTime gameTime)
        {
            if (CachedPosition != Following.Position)
            {
                ReFocus();
                CachedPosition = Following.Position;
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

        public abstract void Zoom(float zoomBy);

        public abstract void ReFocus();
    }
}
