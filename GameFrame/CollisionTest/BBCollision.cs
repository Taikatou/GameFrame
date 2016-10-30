using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.CollisionTest
{
    public class BBCollision : Game, ISubject
    {
        private List<IObserver> observers;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D gameBackground;
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

        public BBCollision(BBObject object1, Texture2D gameBackground)
        {
            this.object1 = object1;
            this.gameBackground = gameBackground;
        }

        public BBCollision(BBObject object1, BBObject object2)
        {
            this.object1 = object1;
            this.object2 = object2;
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
            topWall = new BBObject(gameBackground, Vector2.Zero);
            bottomWall = new BBObject(gameBackground, new Vector2(0, Window.ClientBounds.Height));

            if (object1.BoundingBox.Intersects(topWall.BoundingBox))
            {
                NotifyObservers();
            }

            if (object1.BoundingBox.Intersects(bottomWall.BoundingBox))
            {
                NotifyObservers();
            }

            if (object1.Position.X < object1.BoundingBox.Width)
            {
                NotifyObservers();
            }

            if (object1.Position.X > Window.ClientBounds.Width)
            {
                NotifyObservers();
            }
        }
    }
}