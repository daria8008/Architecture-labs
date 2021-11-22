using System;
using System.Collections.Generic;

namespace ServicesLocator
{
    public static class ServiceRegistry
    {
        public static string GetServiceEndpoint(string serviceName)
        {
            if (_endpointsStorage.TryGetValue(serviceName, out var endpoint))
            {
                return endpoint;
            }
            throw new InvalidOperationException($"There is no endpoint for service: {serviceName}");
        }

        private static readonly IReadOnlyDictionary<string, string> _endpointsStorage = new Dictionary<string, string>()
        {
            { ServicesNames.SupplierSvc1, "http://localhost:5001" },
            { ServicesNames.SupplierSvc2, "http://localhost:5002" },
        };
    }
}
