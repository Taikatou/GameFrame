using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace GameFrame.CollisionTest
{
    public class BbCollision : IUpdate
    {
        private string _collisionType;
        private readonly List<BbObject> _bbObjects;
        readonly BbCollisionSubject _collisionSubject = new BbCollisionSubject();

        public int Height { get; set; }
        public int Width { get; set; }

        public BbCollision(List<BbObject> list, int width, int height)
        {
            _bbObjects = list;
            Height = height;
            Width = width;
            var observer1 = new BbCollisionObserver(_collisionSubject);
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

        public BbCollisionSubject GetBbCollisionSubject()
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
            foreach (BbObject t in _bbObjects)
            {
                if (t.Position.Y < 0)
                {
                    _collisionType = "Top";
                    _collisionSubject.SetCollisionType(_collisionType);
                }

                else if (t.Position.Y > Height - t.BoundingBox.Height)
                {
                    _collisionType = "Bottom";
                    _collisionSubject.SetCollisionType(_collisionType);
                }

                else if (t.Position.X < 0)
                {
                    _collisionType = "Left";
                    _collisionSubject.SetCollisionType(_collisionType);
                }

                else if (t.Position.X + t.BoundingBox.Width > Width)
                {
                    _collisionType = "Right";
                    _collisionSubject.SetCollisionType(_collisionType);
                }
            }
        }

    }
}