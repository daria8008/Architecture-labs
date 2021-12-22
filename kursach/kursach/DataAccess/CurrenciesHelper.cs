using kursach.DataAccess.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace kursach.DataAccess
{
    public static class CurrenciesHelper
    {
        public static readonly HashSet<string> SupportedCurrencies = new HashSet<string> { "USD", "EUR", "RUB", "RUR" };

        public static void NormalizeCurrencies(this IEnumerable<CurrencyExchangeItem> items)
        {
            items.ToList().ForEach(x => x.Currency = x.Currency.Replace("RUR", "RUB"));
        }
    }
}
