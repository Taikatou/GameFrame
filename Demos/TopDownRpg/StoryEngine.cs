using System.Collections.Generic;
using Demos.TopDownRpg.Entities;
using Demos.TopDownRpg.GameModes;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg
{
    public delegate void StoryEvent(AddEntity addEntity, RemoveEntity removeEntity);
    public class StoryEngine
    {
        private readonly Dictionary<string, StoryEvent> _worldLoadEvents;
        private readonly Move _moveDelegate;

        private readonly EntityManager _entityManager;
        public StoryEngine(Move moveDelegate, EntityManager entityManager)
        {
            _moveDelegate = moveDelegate;
            _entityManager = entityManager;
            _worldLoadEvents = new Dictionary<string, StoryEvent>
            {
                ["west_forest_west_entrance"] = (addEntity, removeEntity) =>
                {
                    var princessKidnapped = GameFlags.GetFlag<bool>("princess_kidnapped");
                    if (!princessKidnapped)
                    {
                        var guard = new FakeGuardEntity {Position = new Vector2(14, 8), MoveDelegate = _moveDelegate};
                        var princess = new PrincessPreKidnapping(guard, removeEntity)
                        {
                            Position = new Vector2(14, 10),
                            MoveDelegate = _moveDelegate
                        };
                        addEntity.Invoke(princess);
                        addEntity.Invoke(guard);
                    }
                },
                ["west_forest"] = (addEntity, removeEntity) =>
                {
                    var swordBlocker = new SwordBlocker("sword_blocker_moved", new Vector2(2, 21), new Vector2(2, 20))
                    {
                        MoveDelegate = moveDelegate,
                    };
                    addEntity.Invoke(swordBlocker);
                },
                ["northern_desert"] = (addEntity, removeEntity) =>
                {
                    var guard = new NorthDesertGuard("first_guard_defeated", new Vector2(23, 35), new Vector2(24, 35))
                    {
                        MoveDelegate = moveDelegate,
                    };
                    addEntity.Invoke(guard);
                }
            };

        }

        public void LoadWorld(AddEntity addEntity, RemoveEntity removeEntity, string worldName)
        {
            if (_worldLoadEvents.ContainsKey(worldName))
            {
                _worldLoadEvents[worldName]?.Invoke(addEntity, removeEntity);
            }
        }
    }
}
