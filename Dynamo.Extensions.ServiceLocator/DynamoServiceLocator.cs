using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using Dynamo.Ioc;

namespace Dynamo.Extensions.ServiceLocator
{
    public class DynamoServiceLocator : IServiceLocator
    {
        public DynamoServiceLocator(IocContainer container)
        {
            _container = container;
        }

        private IocContainer _container;

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.ResolveAll<TService>();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.ResolveAll(serviceType);
        }

        public TService GetInstance<TService>(string key)
        {
            return _container.Resolve<TService>(key);
        }

        public TService GetInstance<TService>()
        {
            return _container.Resolve<TService>();
        }

        public object GetInstance(Type serviceType, string key)
        {
            return _container.Resolve(serviceType, key);
        }

        public object GetInstance(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }
    }
}
