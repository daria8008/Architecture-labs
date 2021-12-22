using kursach.DataAccess.Contracts;

namespace kursach.ViewModels
{
    public class CurrencyViewModel
    {
        public string currency { get; set; }
        public double? buy { get; set; }
        public double? sale { get; set; }
    }

    public static class CurrencyViewModelExtensions
    {
        public static CurrencyViewModel ToViewModel(this CurrencyExchangeItem item)
        {
            return new CurrencyViewModel()
            {
                currency = item.Currency,
                buy = item.Buy,
                sale = item.Sale
            };
        }
    }
}
