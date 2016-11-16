using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.CollisionTest
{
    public class BBCollision : IUpdate
    {
        private string _collisionType;
        private Vector2 _velocity;
        private readonly List<BBObject> _bbObjects;
        readonly BBCollisionSubject _collisionSubject = new BBCollisionSubject();

        public int Height { get; set; }
        public int Width { get; set; }

        public BBCollision(List<BBObject> _list, int width, int height)
        {
            _bbObjects = _list;
            Height = height;
            Width = width;
            var observer1 = new BBCollisionObserver(_collisionSubject);
        }

        public void Update(GameTime gametime)
        {
            CheckCollision();
            CheckWallCollision();
        }

        /*public void SetVelocity(Vector2 vel)
        {
            _velocity = vel;
        }*/

        public BBCollisionSubject GetBbCollisionSubject()
        {
            return _collisionSubject;
        }

        public void CheckCollision()
        {
            for (int i = 0; i < _bbObjects.Count; i++)
            {
                for (int j = i + 1; j < _bbObjects.Count; j++)
                {
                    if (_bbObjects[i].BoundingBox.Intersects(_bbObjects[j].BoundingBox))
                    {
                        _collisionType = "Object";
                        //_velocity.X = _velocity.X*-1;
                        _collisionSubject.SetCollisionType(_collisionType);
                        //_collisionSubject.SetVelocity(_velocity);
                    }
                    else
                    {
                        CheckWallCollision();
                    }
                }
            }
        }

        public void CheckWallCollision()
        {
            for (int i = 0; i < _bbObjects.Count; i++)
            {
                if (_bbObjects[i].Position.Y < 0)
                {
                    _collisionType = "Top";
                    _collisionSubject.SetCollisionType(_collisionType);
                }

                else if (_bbObjects[i].Position.Y > Height - _bbObjects[i].BoundingBox.Height)
                {
                    _collisionType = "Bottom";
                    _collisionSubject.SetCollisionType(_collisionType);
                }

                else if (_bbObjects[i].Position.X < 0)
                {
                    _collisionType = "Left";
                    _collisionSubject.SetCollisionType(_collisionType);
                }

                else if (_bbObjects[i].Position.X + _bbObjects[i].BoundingBox.Width > Width)
                {
                    _collisionType = "Right";
                    _collisionSubject.SetCollisionType(_collisionType);
                }
            }

        }

    }
}