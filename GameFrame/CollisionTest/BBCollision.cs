using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;

namespace GameFrame.CollisionTest
{
    public class BBCollision : Game, ISubject
    {
        private List<IObserver> observers;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BBObject object1;
        BBObject object2;
        BBObject topWall;
        BBObject bottomWall;
        BBObject leftWall;
        BBObject rightWall;

        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void UnRegisterObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach(IObserver ob in observers)
            {
                ob.Update();
            }
        }

        public BBCollision()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Update(GameTime gametime)
        {
            CheckWallCollision();
            CheckObjectCollision();

            base.Update(gametime);
        }

        public void CheckObjectCollision()
        {
            if (object1.BoundingBox.Intersects(object2.BoundingBox))
            {
                NotifyObservers();
            }
        }

        private void CheckWallCollision()
        {
            if (object1.BoundingBox.Intersects(topWall.BoundingBox))
            {
                NotifyObservers();
            }
            if (object1.BoundingBox.Intersects(bottomWall.BoundingBox))
            {
                NotifyObservers();
            }
            if (object1.Position.X < -object1.BoundingBox.Width)
            {
                NotifyObservers();
            }

            if (object1.Position.X > object1.BoundingBox.Width)
            {
                NotifyObservers();
            }
        }
    }
}

