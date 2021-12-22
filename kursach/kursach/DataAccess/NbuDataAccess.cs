using kursach.DataAccess.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace kursach.DataAccess
{
    public class NbuDataAccess : INbuDataAccess
    {
        public async Task<List<CurrencyExchangeItem>> GetCurrencyDataAsync()
        {
            const string Url = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
            var resp = await _httpClient.GetAsync(Url);
            var json = await resp.Content.ReadAsStringAsync();
            var allData = JsonSerializer.Deserialize<List<NbuCurrencyItem>>(json);
            var fileteredCurrencies = allData
                .Where(x => CurrenciesHelper.SupportedCurrencies.Contains(x.cc))
                .Select(x => x.ToContract())
                .ToList();

            return fileteredCurrencies;
        }

        private readonly HttpClient _httpClient = new HttpClient();

        private class NbuCurrencyItem
        {
            public string cc { get; set; }
            public double rate { get; set; }

            public CurrencyExchangeItem ToContract()
            {
                return new CurrencyExchangeItem()
                {
                    Currency = cc,
                    Buy = rate
                };
            }
        }
    }
}
