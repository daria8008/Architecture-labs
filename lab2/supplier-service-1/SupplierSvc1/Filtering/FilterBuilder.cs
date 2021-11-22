using System;
using System.Collections.Generic;

namespace SupplierSvc1.Filtering
{
    public class FilterBuilder
    {
        public FilterBuilderOptions For<T>() where T : class
        {
            var type = typeof(T);
            if (_filters.ContainsKey(type))
            {
                throw new InvalidOperationException($"Type {type} was already configured");
            }

            var propFilters = new Dictionary<string, Func<object, string, bool>>();
            _filters[type] = propFilters;
            return new FilterBuilderOptions(type, propFilters);
        }

        public FilterRules Build()
        {
            return new FilterRules(_filters);
        }

        private readonly Dictionary<Type, Dictionary<string, Func<object, string, bool>>> _filters = new();
    }
}
