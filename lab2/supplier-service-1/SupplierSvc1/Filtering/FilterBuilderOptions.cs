using System;
using System.Collections.Generic;

namespace SupplierSvc1.Filtering
{
    public class FilterBuilderOptions
    {
        public FilterBuilderOptions(Type type, Dictionary<string, Func<object, string, bool>> propFilters)
        {
            _type = type;
            _propFilters = propFilters;
        }

        public FilterBuilderOptions StringContainsStringIgnoreCase(string propName)
        {
            Func<object, string, bool> filter = (src, comparandValue) =>
            {
                var value = (string)GetPropValue(src, propName);
                return value.Contains(comparandValue, StringComparison.InvariantCultureIgnoreCase);
            };
            _propFilters[propName] = filter;
            return this;
        }

        public FilterBuilderOptions StringEqualsStringIgnoreCase(string propName)
        {
            Func<object, string, bool> filter = (src, comparandValue) =>
            {
                var value = (string)GetPropValue(src, propName);
                return value.ToLowerInvariant() == comparandValue.ToLowerInvariant();
            };
            _propFilters[propName] = filter;
            return this;
        }

        public FilterBuilderOptions IntIsEqualOrGreaterThan(string propName)
        {
            Func<object, string, bool> filter = (src, comparandValue) =>
            {
                var value = (int)GetPropValue(src, propName);
                return value >= (Convert.ToInt32(comparandValue));
            };
            _propFilters[propName] = filter;
            return this;
        }

        private object GetPropValue(object src, string propName)
        {
            return _type.GetProperty(propName).GetValue(src, null);
        }

        private readonly Type _type;
        private readonly Dictionary<string, Func<object, string, bool>> _propFilters;
    }
}
