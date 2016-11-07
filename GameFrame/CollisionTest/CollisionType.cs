using System.Diagnostics;


namespace GameFrame.CollisionTest
{
    public class CollisionType
    {
        private string type;

        public CollisionType()
        {
        }

        public void Update(string collisionType)
        {
            this.type = collisionType;
        }

        public string getCollisionType()
        {
            return type;
        }

        public void setCollisionType(string collisionType)
        {
            Debug.WriteLine("" + collisionType);
        }
    }
}
