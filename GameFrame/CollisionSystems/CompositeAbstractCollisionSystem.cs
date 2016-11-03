using System.Collections.Generic;
using GameFrame.PathFinding.PossibleMovements;
using Microsoft.Xna.Framework;

namespace GameFrame.CollisionSystems
{
    public class CompositeAbstractCollisionSystem : AbstractCollisionSystem
    {
        private readonly List<AbstractCollisionSystem> _collisionSystems;

        public CompositeAbstractCollisionSystem(IPossibleMovements possibleMovements) : base(possibleMovements)
        {
            _collisionSystems = new List<AbstractCollisionSystem>();
        }

        public bool Contains(AbstractCollisionSystem system)
        {
            var contains = _collisionSystems.Contains(system);
            return contains;
        }

        public void AddCollisionSystem(AbstractCollisionSystem system)
        {
            if (!Contains(system))
            {
                _collisionSystems.Add(system);
            }
        }

        public void RemoveCollisionSystem(AbstractCollisionSystem system)
        {
            if (Contains(system))
            {
                _collisionSystems.Remove(system);
            }
        }

        public override bool CheckCollision(Point position)
        {
            var found = false;
            foreach (var system in _collisionSystems)
            {
                found = found || system.CheckCollision(position);
            }
            return found;
        }
    }
}
