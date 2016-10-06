using System.Collections.Generic;

namespace GameFrame.CollisionSystems
{
    public class CompositeCollisionSystem : ICollisionSystem
    {
        private readonly List<ICollisionSystem> _collisionSystems;

        public CompositeCollisionSystem()
        {
            _collisionSystems = new List<ICollisionSystem>();
        }

        public void AddCollisionSystem(ICollisionSystem system)
        {
            _collisionSystems.Add(system);
        }

        public void RemoveCollisionSystem(ICollisionSystem system)
        {
            _collisionSystems.Remove(system);
        }

        public bool CheckCollision(int x, int y)
        {
            var found = false;
            foreach (var system in _collisionSystems)
            {
                found = system.CheckCollision(x, y);
                if (found)
                {
                    break;
                }
            }
            return found;
        }
    }
}
