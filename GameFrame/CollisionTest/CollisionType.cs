using System.Diagnostics;


namespace GameFrame.CollisionTest
{
    public class CollisionType
    {
        public string Type { get; set; }

        public void Update(string collisionType)
        {
            Type = collisionType;
        }
    }
}
