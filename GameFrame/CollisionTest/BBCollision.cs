using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.CollisionTest
{
    public class BBCollision : IUpdate
    {
        private string _collisionType;
        private readonly BBObject _object1;
        private readonly BBObject _object2;
        private readonly BBObject _object3;
        private Vector2 _velocity;
        readonly BBCollisionSubject _collisionSubject = new BBCollisionSubject();

        public int Height { get; set; }
        public int Width { get; set; }
        

        public BBCollision()
        {
            var observer1 = new BBCollisionObserver(_collisionSubject);
            _collisionSubject.RegisterObserver(observer1);
            _collisionType = "";
        }

        public BBCollision(BBObject object1, BBObject object2)
        {
            var observer1 = new BBCollisionObserver(_collisionSubject);
            _collisionSubject.RegisterObserver(observer1);
            _object1 = object1;
            _object2 = object2;
        }

        public BBCollision(BBObject object1, BBObject object2, BBObject object3, int width, int height)
        {
            Height = height;
            Width = width;
            var observer1 = new BBCollisionObserver(_collisionSubject);
            _collisionSubject.RegisterObserver(observer1);
            _object1 = object1;
            _object2 = object2;
            _object3 = object3;
        }

        public void Update(GameTime gametime)
        {
            CheckOnePlayerCollision();
            CheckTwoPlayerCollision();
            CheckWallCollision();
        }

        public void SetVelocity(Vector2 vel)
        {
            _velocity = vel;
        }

        public BBCollisionSubject GetBbCollisionSubject()
        {
            return _collisionSubject;
        }

        public void CheckOnePlayerCollision()
        {
            if (_object1.BoundingBox.Intersects(_object2.BoundingBox))
            {
                _collisionType = "Object";
                _velocity.X = _velocity.X*-1;
                _collisionSubject.SetCollisionType(_collisionType);
                _collisionSubject.SetVelocity(_velocity);
            }
            else
            {
                CheckWallCollision();
            }
        }

        public void CheckTwoPlayerCollision()
        {
            if (_object1.BoundingBox.Intersects(_object2.BoundingBox) ||
                _object1.BoundingBox.Intersects(_object3.BoundingBox))
            {
                _collisionType = "Object";
                _velocity.X = _velocity.X*-1;
                _collisionSubject.SetCollisionType(_collisionType);
                _collisionSubject.SetVelocity(_velocity);
            }
            else
            {
                CheckWallCollision();
            }
        }

        public void CheckWallCollision()
        {
           
            if (_object1.BoundingBox.Intersects(_object2.BoundingBox) || _object1.BoundingBox.Intersects(_object3.BoundingBox))
            {
                _collisionType = "Object";
                _velocity.X = _velocity.X * - 1;
                _collisionSubject.SetCollisionType(_collisionType);
                _collisionSubject.SetVelocity(_velocity);
            }

            else if (_object1.Position.Y < 0)
            {
                _collisionType = "Top";
                _velocity.Y *= -1;
                _collisionSubject.SetCollisionType(_collisionType);
                _collisionSubject.SetVelocity(_velocity);
            }

            else if (_object1.Position.Y > Height - _object1.BoundingBox.Height)
            {
                _collisionType = "Bottom";
                _velocity.Y *= -1;
                _collisionSubject.SetCollisionType(_collisionType);
                _collisionSubject.SetVelocity(_velocity);
            }

            else if (_object1.Position.X < 0)
            {
                _collisionType = "Left";
                _velocity.X = _velocity.X * -1;
                _collisionSubject.SetCollisionType(_collisionType);
                _collisionSubject.SetVelocity(_velocity);
            }

            else if (_object1.Position.X + _object1.BoundingBox.Width > Width)
            {
                _collisionType = "Right";
                _velocity.X = _velocity.X * -1;
                _collisionSubject.SetCollisionType(_collisionType);
                _collisionSubject.SetVelocity(_velocity);
            }
        }
       
    }
}