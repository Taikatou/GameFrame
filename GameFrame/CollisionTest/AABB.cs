using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.CollisionTest
{
    class AABB
    {
        public Vector2d position;
        private float length;
        private float width;

        public AABB(float x, float y, float length, float width)
        {
            this.length = length;
            this.width = width;

        }

        public void setLength(float length)
        {
            this.length = length;
        }
        public void setWidth(float width)
        {
            this.width = width;
        }

        public float getLength()
        {
            return length;
        }
        public float getWidth()
        {
            return width;
        }

        public Boolean colliding(AABB aabb)
        {
            //this method need work, will finish later
            float x2Pos = position.getX() + length;
            float y2Pos = position.getY() + width;

            float x1diff = position.getX() - aabb.position.getX();
            float y1diff = position.getY() - aabb.position.getY();

            float x2diff = position.getX() - aabb.position.getX();
            float y2diff = position.getY() - aabb.position.getY();

            //only checks if one of the corners are the exact same as the other objectss co-ordinates
            if (x1diff == 0 || y1diff == 0 || x2diff == 0 || y2diff == 0) {
                return true;
            }

            return false;
           

        }

        public int compareTo(AABB aabb)
        {
            if ((this.position.getX() - this.getLength() > aabb.position.getX()) || (this.position.getX() - this.getWidth() > aabb.position.getX()))
            {
                return 1;
            }

            else if ((this.position.getX() - this.getLength() < aabb.position.getX()) || (this.position.getX() - this.getWidth() < aabb.position.getX()))
            {
                return -1;
            }

            else
            {
                return 0;
            }
        }



    }
}
