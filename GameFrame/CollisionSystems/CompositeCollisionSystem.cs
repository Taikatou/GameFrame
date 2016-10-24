using System.Collections.Generic;
using Microsoft.Xna.Framework;

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

        public bool CheckCollision(Point point)
        {
            var found = false;
            foreach (var system in _collisionSystems)
            {
                found = found || system.CheckCollision(point);
            }
            return found;
        }
    }
}
