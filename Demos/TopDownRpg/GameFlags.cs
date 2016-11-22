using System.Collections.Generic;

namespace Demos.TopDownRpg
{
    public class GameFlags
    {
        private readonly Dictionary<string, object> _variables;

        public GameFlags()
        {
            _variables = new Dictionary<string, object>();
        }

        public void AddObject<T>(string variableName, T variable)
        {
            _variables[variableName] = variable;
        }

        public T GetFlag<T>(string variableName, T defaultValue=default(T))
        {
            T toReturn = defaultValue;
            if (_variables.ContainsKey(variableName))
            {
                toReturn = (T)_variables[variableName];
            }
            return toReturn;
        }
    }
}
