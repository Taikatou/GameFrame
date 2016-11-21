using System.Collections.Generic;
using GameFrame.ServiceLocator;
using GameFrame.Services;
using Newtonsoft.Json;

namespace Demos.TopDownRpg
{
    public class EntityManager
    {
        private readonly Dictionary<string, Entity> _loadedEntities;
        public EntityManager()
        {
            _loadedEntities = new Dictionary<string, Entity>();
        }

        public Entity Import(string fileName)
        {
            if(!_loadedEntities.ContainsKey(fileName))
            {
                var textReader = StaticServiceLocator.GetService<ISaveAndLoad>();
                var jsonText = textReader.LoadText($"Entities/{fileName}.json");
                _loadedEntities[fileName] = JsonConvert.DeserializeObject<Entity>(jsonText);
            }
            return _loadedEntities[fileName];
        }
    }
}
