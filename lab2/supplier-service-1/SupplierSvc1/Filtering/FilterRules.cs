using System;
using System.Collections.Generic;

namespace SupplierSvc1.Filtering
{
    public class FilterRules
    {
        public FilterRules(IReadOnlyDictionary<Type, Dictionary<string, Func<object, string, bool>>> rules)
        {
            _rules = rules;
        }

        public Func<T, string, bool> GetFor<T>(string propName)
        {
            var type = typeof(T);
            if (!_rules.TryGetValue(type, out var typeFilter))
            {
                throw new InvalidOperationException($"Type {type} was not configured");
            }
            var propFilterRule = typeFilter[propName];

            return (src, comparandValue) => propFilterRule(src, comparandValue);
        }

        private readonly IReadOnlyDictionary<Type, Dictionary<string, Func<object, string, bool>>> _rules;
    }
}
