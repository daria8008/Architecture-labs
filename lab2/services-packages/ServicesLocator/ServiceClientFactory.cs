using ServicesLocator.Clients;
using System;
using System.Collections.Generic;

namespace ServicesLocator
{
    public class ServiceClientFactory
    {
        public ServiceClientFactory()
        {
            _clientFactories = new Dictionary<Type, Func<object>>
            {
                {
                    typeof(SupplierSvc1Client),
                    () =>
                    {
                        var serviceEndpoint = ServiceRegistry.GetServiceEndpoint(ServicesNames.SupplierSvc1);
                        return new SupplierSvc1Client(serviceEndpoint);
                    }
                },
                {
                    typeof(SupplierSvc2Client),
                    () =>
                    {
                        var serviceEndpoint = ServiceRegistry.GetServiceEndpoint(ServicesNames.SupplierSvc2);
                        return new SupplierSvc2Client(serviceEndpoint);
                    }
                }
            };
        }

        public TService ResolveClient<TService>()
        {
            if (!_clientFactories.TryGetValue(typeof(TService), out var clientFactoryFunc))
            {
                throw new InvalidOperationException($"Factory not found: {typeof(TService)}");
            }

            return (TService)clientFactoryFunc();
        }

        private readonly IReadOnlyDictionary<Type, Func<object>> _clientFactories;
    }
}
