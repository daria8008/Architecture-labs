using kursach.DataAccess.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace kursach.ViewModels
{
    public class CurrencyDataItemViewModel
    {
        public string bankName { get; set; }
        public List<CurrencyViewModel> data { get; set; }
    }

    public static class CurrencyDataItemViewModelExtensions
    {
        public static CurrencyDataItemViewModel ToViewModel(this IEnumerable<CurrencyExchangeItem> items, string bankName)
        {
            return new CurrencyDataItemViewModel()
            {
                bankName = bankName,
                data = items.Select(x => x.ToViewModel()).ToList()
            };
        }
    }
}
