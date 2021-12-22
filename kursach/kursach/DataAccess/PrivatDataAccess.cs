using kursach.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace kursach.DataAccess
{
    public class PrivatDataAccess : IPrivatDataAccess
    {
        public async Task<List<CurrencyExchangeItem>> GetCurrencyDataAsync()
        {
            const string Url = "https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=11";
            var resp = await _httpClient.GetAsync(Url);
            var json = await resp.Content.ReadAsStringAsync();
            var allData = JsonSerializer.Deserialize<List<PrivatCurrencyItem>>(json);
            var fileteredData = allData
                .Where(x => CurrenciesHelper.SupportedCurrencies.Contains(x.ccy))
                .Select(x => x.ToContract())
                .ToList();

            return fileteredData;
        }

        private readonly HttpClient _httpClient = new HttpClient();

        private class PrivatCurrencyItem
        {
            public string ccy { get; set; }
            public string base_ccy { get; set; }
            public string buy { get; set; }
            public string sale { get; set; }

            public CurrencyExchangeItem ToContract()
            {
                return new CurrencyExchangeItem()
                {
                    Currency = ccy,
                    Buy = Convert.ToDouble(buy),
                    Sale = Convert.ToDouble(sale)
                };
            }
        }
    }
}
