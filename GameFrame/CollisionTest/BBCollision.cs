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
    public class BBCollision : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        BBObject object1;
        BBObject object2;
        BBObject topWall;
        BBObject bottomWall;
        BBObject leftWall;
        BBObject rightWall;

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

        private void CheckObjectCollision()
        {
            if (object1.BoundingBox.Intersects(object2.BoundingBox))
            {

            }
        }

        private void CheckWallCollision()
        {
            if (object1.BoundingBox.Intersects(topWall.BoundingBox))
            {

            }
            if (object1.BoundingBox.Intersects(bottomWall.BoundingBox))
            {

            }
            if (object1.BoundingBox.Intersects(leftWall.BoundingBox))
            {

            }
            if (object1.BoundingBox.Intersects(rightWall.BoundingBox))
            {

            }
        }





    }
}
