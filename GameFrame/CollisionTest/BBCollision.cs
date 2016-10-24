using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
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
        BBObject player;
        BBObject enemy;
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
            if (player.BoundingBox.Intersects(enemy.BoundingBox))
            {
                NotifyObservers();
                Debug.WriteLine("Collision Detected with another object");
            }
        }

        private void CheckWallCollision()
        {
            if (player.BoundingBox.Intersects(topWall.BoundingBox))
            {
                NotifyObservers();
                Debug.WriteLine("Colision with top wall");
            }
            if (player.BoundingBox.Intersects(bottomWall.BoundingBox))
            {
                NotifyObservers();
                Debug.WriteLine("Colision with bottom wall");
            }
            if (player.BoundingBox.Intersects(leftWall.BoundingBox))
            {
                NotifyObservers();
                Debug.WriteLine("Colision with left wall");
            }
            if (player.BoundingBox.Intersects(rightWall.BoundingBox))
            {
                NotifyObservers();
                Debug.WriteLine("Colision with right wall");
            }
        }
    }
}
