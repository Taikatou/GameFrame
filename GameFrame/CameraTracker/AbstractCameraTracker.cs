using GameFrame.Common;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace GameFrame.CameraTracker
{
    public abstract class AbstractCameraTracker : IUpdate, IMovable
    {
        public IFocusAble Following;
        public Vector2 CachedPosition;
        public static float CameraZoomBaseValue = 2.0f;
        public Camera2D Camera;
        public Matrix TransformationMatrix => Camera.GetViewMatrix();
        public Vector2 Position
        {
            get
            {
                return Camera.Position;
            }
            set
            {
                Camera.Position = value;
            }
        }
        public void Zoom(float zoomBy)
        {
            CameraZoom += zoomBy;
        }
        public abstract void ReFocus();

        protected AbstractCameraTracker(ViewportAdapter viewPort, IFocusAble following)
        {
            Camera = new Camera2D(viewPort) { Zoom = CameraZoomBaseValue };
            Following = following;
        }

        public void Update(GameTime gameTime)
        {
            if (CachedPosition != Following.Position)
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

        public float CameraZoom
        {
            get
            {
                return CameraZoomBaseValue;
            }
            set
            {
                if (value >= Camera.MinimumZoom && value <= Camera.MaximumZoom)
                {
                    CameraZoomBaseValue = value;
                    Camera.Zoom = CameraZoomBaseValue;
                    ReFocus();
                }
            }
        }
    }
}
