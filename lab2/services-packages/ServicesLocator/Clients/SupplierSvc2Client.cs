using ServicesContracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServicesLocator.Clients
{
    public class SupplierSvc2Client
    {
        public SupplierSvc2Client(string baseUrl)
        {
            _baseUrl = new Uri(baseUrl);
            _httpClient = new HttpClient();
        }

        public async Task<List<Vacancy>> GetVacanciesAsync()
        {
            var requestUri = new Uri(_baseUrl, "/vacancies/list");
            var response = await _httpClient.GetAsync(requestUri);
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

        private readonly Uri _baseUrl;
        private readonly HttpClient _httpClient;
    }
}
