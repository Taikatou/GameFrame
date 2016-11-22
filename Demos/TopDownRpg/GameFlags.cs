using System.Collections.Generic;

namespace Demos.TopDownRpg
{
    public class GameFlags
    {
        private static GameFlags _instance;
        public static GameFlags Instance => _instance ?? (_instance = new GameFlags());
        private readonly Dictionary<string, object> _variables;

        protected GameFlags()
        {
            _variables = new Dictionary<string, object>();
        }

        public static void AddObject<T>(string variableName, T variable)
        {
            Instance._variables[variableName] = variable;
        }

        public static T GetFlag<T>(string variableName, T defaultValue=default(T))
        {
            T toReturn = defaultValue;
            if (Instance._variables.ContainsKey(variableName))
            {
                toReturn = (T)Instance._variables[variableName];
            }
            return toReturn;
        }
    }
}
