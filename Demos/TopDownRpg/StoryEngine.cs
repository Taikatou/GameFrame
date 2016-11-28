using System.Collections.Generic;
using Demos.TopDownRpg.Entities;
using Demos.TopDownRpg.GameModes;
using GameFrame;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg
{
    public delegate void StoryEvent(AddEntity addEntity, RemoveEntity removeEntity);
    public class StoryEngine
    {
        private readonly Dictionary<string, StoryEvent> _worldLoadEvents;
        private readonly Move _moveDelegate;

        private readonly EntityManager _entityManager;
        public StoryEngine(GameModeController gameModeController, Move moveDelegate, EntityManager entityManager)
        {
            _moveDelegate = moveDelegate;
            _entityManager = entityManager;
            _worldLoadEvents = new Dictionary<string, StoryEvent>
            {
                ["west_forest_west_entrance"] = (addEntity, removeEntity) =>
                {
                    var princessKidnapped = GameFlags.GetVariable<bool>("princess_kidnapped");
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
                    var guard = new NorthDesertGuard(gameModeController, "first_guard_defeated", new Vector2(23, 35), new Vector2(24, 35))
                    {
                        MoveDelegate = moveDelegate
                    };
                    addEntity.Invoke(guard);
                    var hideoutGuard = new HideoutGuard("hideout_guard.ink", gameModeController, "second_guard_defeated", new Vector2(21, 6), new Vector2(21, 5))
                    {
                        MoveDelegate = moveDelegate
                    };
                    addEntity.Invoke(hideoutGuard);
                },
                ["north_desert_hideout_second_floor"] = (addEntity, removeEntity) =>
                {
                    var hideoutGuard = new HideoutGuard("second_hideout_guard.ink", gameModeController, "third_guard_defeated", new Vector2(21, 12), new Vector2(20, 15))
                    {
                        MoveDelegate = moveDelegate
                    };
                    addEntity.Invoke(hideoutGuard);
                    var secondHideoutGuard = new HideoutGuard("hideout_guard.ink", gameModeController, "forth_guard_defeated", new Vector2(18, 12), new Vector2(17, 15))
                    {
                        MoveDelegate = moveDelegate
                    };
                    addEntity.Invoke(secondHideoutGuard);
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
