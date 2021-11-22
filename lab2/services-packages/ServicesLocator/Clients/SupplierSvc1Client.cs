using ServicesContracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServicesLocator.Clients
{
    public class SupplierSvc1Client
    {
        public SupplierSvc1Client(string baseUrl)
        {
            _baseUrl = new Uri(baseUrl);
            _httpClient = new HttpClient();
        }

        public async Task<List<Vacancy>> GetVacanciesByFilterAsync(Dictionary<string, string> filters)
        {
            var requestUri = new UriBuilder(_baseUrl + "search");
            AddQuery(requestUri, filters);

            var response = await _httpClient.GetAsync(requestUri.Uri);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var body = await response.Content.ReadAsStringAsync();
                var serializerOptions = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<List<Vacancy>>(body, serializerOptions);
            }

            throw new InvalidOperationException($"Failed to get vacancies from {requestUri}");
        }

        private static void AddQuery(UriBuilder builder, Dictionary<string, string> queryParams)
        {
            var query = "";

            foreach (var kv in queryParams)
            {
                query += string.Format("{0}={1}&", kv.Key, kv.Value);
            }

            if (query.Length > 0)
            {
                query.Remove(query.Length - 1, 1);
            }

            builder.Query = query.ToString();
        }

        private readonly Uri _baseUrl;
        private readonly HttpClient _httpClient;
    }
}
