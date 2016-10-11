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

        public bool Contains(ICollisionSystem system)
        {
            var contains = _collisionSystems.Contains(system);
            return contains;
        }

        public void AddCollisionSystem(ICollisionSystem system)
        {
            if (!Contains(system))
            {
                _collisionSystems.Add(system);
            }
        }

        public void RemoveCollisionSystem(ICollisionSystem system)
        {
            if (Contains(system))
            {
                _collisionSystems.Remove(system);
            }
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
