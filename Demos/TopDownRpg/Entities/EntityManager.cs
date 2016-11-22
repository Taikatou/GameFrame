using System.Collections.Generic;
using GameFrame.ServiceLocator;
using GameFrame.Services;
using Newtonsoft.Json;

namespace Demos.TopDownRpg.Entities
{
    public class EntityManager
    {
        private readonly Dictionary<string, Entity> _loadedEntities;
        private readonly Move _moveDelegate;
        public EntityManager(Move moveDelegate, GameFlags gameFlags)
        {
            _moveDelegate = moveDelegate;
            _loadedEntities = new Dictionary<string, Entity>
            {
                ["villager"] = new VillagerEntity { MoveDelegate = moveDelegate },
                ["dojo_master"] = new DojoMasterEntity { MoveDelegate = moveDelegate },
                ["black_smith"] = new BlackSmithEntity(gameFlags) { MoveDelegate = moveDelegate },
                ["sword_blocker"] = new SwordBlocker(gameFlags) { MoveDelegate = moveDelegate },
                ["princess"] = new Princess { MoveDelegate = moveDelegate }
            };
        }

        public Entity Import(string fileName)
        {
            if(!_loadedEntities.ContainsKey(fileName))
            {
                var textReader = StaticServiceLocator.GetService<ISaveAndLoad>();
                var jsonText = textReader.LoadText($"Entities/{fileName}.json");
                var entity = JsonConvert.DeserializeObject<NpcEntity>(jsonText);
                entity.MoveDelegate = _moveDelegate;
                _loadedEntities[fileName] = entity;
            }
            return _loadedEntities[fileName];
        }
    }
}
