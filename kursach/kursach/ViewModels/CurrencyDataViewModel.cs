using System.Collections.Generic;

namespace kursach.ViewModels
{
    public class CurrencyDataViewModel : List<CurrencyDataItemViewModel>
    {
        public CurrencyDataViewModel()
        {
        }

        public CurrencyDataViewModel(IEnumerable<CurrencyDataItemViewModel> collection)
            : base(collection)
        {
        }
    }
}
