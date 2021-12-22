using System.Collections.Generic;
using System.Threading.Tasks;

namespace kursach.DataAccess.Contracts
{
    public interface IBankDataAccess
    {
        Task<List<CurrencyExchangeItem>> GetCurrencyDataAsync();
    }
}
