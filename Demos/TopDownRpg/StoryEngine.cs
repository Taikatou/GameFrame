using System.Collections.Generic;
using Demos.TopDownRpg.Entities;
using Demos.TopDownRpg.GameModes;
using GameFrame;
using Microsoft.Xna.Framework;

namespace Demos.TopDownRpg
{
    public delegate void StoryEvent(AddEntity addEntity, RemoveEntity removeEntity, Say sayDelegate, Collision collision);

    public delegate bool Collision(Point startPoint, Point endPoint);
    public class StoryEngine
    {
        private readonly Dictionary<string, StoryEvent> _worldLoadEvents;
        private readonly Move _moveDelegate;
        private readonly Teleport _teleport;
        private readonly Say _say;

        public StoryEngine(GameModeController gameModeController, Move moveDelegate, Teleport teleport, Say say)
        {
            _teleport = teleport;
            _say = say;
            _moveDelegate = moveDelegate;
            _worldLoadEvents = new Dictionary<string, StoryEvent>
            {
                ["west_forest_west_entrance"] = (addEntity, removeEntity, sayDelegate, collisionDelegate) =>
                {
                    if (!Flags.PrincessKidnapped)
                    {
                        var guard = new Entity
                        {
                            Position = new Vector2(14, 8),
                            SpriteSheet = "5",
                            Name ="Guard",
                            Script = "fake_guard.ink"
                        };
                        var princess = new PrincessPreKidnapping(guard, removeEntity)
                        {
                            Position = new Vector2(14, 10),
                            MoveDelegate = _moveDelegate
                        };
                        addEntity.Invoke(princess);
                        addEntity.Invoke(guard);
                    }
                },
                ["west_forest"] = (addEntity, removeEntity, sayDelegate, collisionDelegate) =>
                {
                    var swordBlocker = new SwordBlocker("sword_blocker_moved", new Vector2(2, 21), new Vector2(2, 20), collisionDelegate)
                    {
                        MoveDelegate = moveDelegate,
                    };
                    addEntity.Invoke(swordBlocker);
                },
                ["northern_desert"] = (addEntity, removeEntity, sayDelegate, collisionDelegate) =>
                {
                    if (!Flags.GameComplete)
                    {
                        var guard = new NorthDesertGuard(gameModeController, "first_guard_defeated", new Vector2(23, 35), new Vector2(24, 35), new Vector2(22, 35), collisionDelegate)
                        {
                            MoveDelegate = moveDelegate
                        };
                        addEntity.Invoke(guard);
                        var hideoutGuard = new HideoutGuard("hideout_guard.ink", gameModeController, "second_guard_defeated", new Vector2(21, 6), new Vector2(21, 5), new Vector2(21, 7), collisionDelegate)
                        {
                            MoveDelegate = moveDelegate
                        };
                        addEntity.Invoke(hideoutGuard);
                    }
                },
                ["north_desert_hideout_second_floor"] = (addEntity, removeEntity, sayDelegate, collisionDelegate) =>
                {
                    if (Flags.PrincessKidnapped && !Flags.GameComplete)
                    {
                        var hideoutGuard = new HideoutGuard("second_hideout_guard.ink", gameModeController, "third_guard_defeated", new Vector2(21, 12), new Vector2(20, 15), new Vector2(20, 10), collisionDelegate)
                        {
                            MoveDelegate = moveDelegate
                        };
                        addEntity.Invoke(hideoutGuard);
                        var secondHideoutGuard = new HideoutGuard("hideout_guard.ink", gameModeController, "forth_guard_defeated", new Vector2(18, 12), new Vector2(17, 15), new Vector2(17, 10), collisionDelegate)
                        {
                            MoveDelegate = moveDelegate
                        };
                        addEntity.Invoke(secondHideoutGuard);
                        var princess = new PrincessKidnapped(teleport, sayDelegate)
                        {
                            Position = new Vector2(13, 15),
                            MoveDelegate = moveDelegate
                        };
                        addEntity.Invoke(princess);
                        var dojoMaster = new DojoMasterHideout (gameModeController, "master_defeat", new Vector2(13, 13), new Vector2(12, 13))
                        {
                            MoveDelegate = moveDelegate
                        };
                        addEntity.Invoke(dojoMaster);
                    }
                },
                ["perfect_house"] = (addEntity, removeEntity, sayDelegate, collisionDelegate) =>
                {
                    if (Flags.GameComplete)
                    {
                        var princess = new PrincessSafe
                        {
                            Position = new Vector2(7, 7)
                        };
                        addEntity.Invoke(princess);
                    }
                }
            };

        }

        public void LoadWorld(AddEntity addEntity, RemoveEntity removeEntity, Collision collision, string worldName)
        {
            if (_worldLoadEvents.ContainsKey(worldName))
            {
                _worldLoadEvents[worldName]?.Invoke(addEntity, removeEntity, _say, collision);
            }
        }
    }
}
