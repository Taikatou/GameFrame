using System;
using System.Collections.Generic;

namespace GameFrame.ServiceLocator
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IDictionary<object, object> _services;

        internal ServiceLocator()
        {
            _services = new Dictionary<object, object>();
        }

        public T GetService<T>()
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                throw new Exception("The requested service is not registered");
            }
        }

        public void AddService<T>(T service)
        {
            _services[typeof(T)] = service;
        }
    }
}
