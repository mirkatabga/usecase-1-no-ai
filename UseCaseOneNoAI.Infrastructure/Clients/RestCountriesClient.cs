using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http.Json;
using System.Text.Json;
using UseCaseOneNoAI.Infrastructure.Exceptions;
using UseCaseOneNoAI.Infrastructure.Models;

namespace UseCaseOneNoAI.Infrastructure.Clients
{
    internal class RestCountriesClient : ICountryClient
    {
        private const string ALL_COUNTRIES_KEY = "ALL_COUNTRIES";
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;

        public RestCountriesClient(
            IHttpClientFactory httpClientFactory,
            IDistributedCache cache)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(ICountryClient));
            _cache = cache;
        }

        public async Task<IEnumerable<CountryDataModel>> GetCountries(string? name, CancellationToken cancellationToken)
        {
            var cachedCountries = await GetCached(cancellationToken);

            if (cachedCountries.Any() == true)
            {
                return await Task.FromResult(cachedCountries);
            }

            var countries = await FetchCountries(cancellationToken);
            await SetCache(countries, cancellationToken);

            return countries!;
        }

        private async Task<IEnumerable<CountryDataModel>?> FetchCountries(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync("/v3.1/all", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var reason = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new IntegrationException($"Could not retrive countries. Reason: {reason}");
            }

            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var countries = await response.Content.ReadFromJsonAsync<IEnumerable<CountryDataModel>>(
                jsonOptions, cancellationToken);

            return countries;
        }

        private async Task<IEnumerable<CountryDataModel>> GetCached(CancellationToken cancellationToken)
        {
            var countriesJson = await _cache.GetStringAsync(ALL_COUNTRIES_KEY, cancellationToken);

            if (string.IsNullOrWhiteSpace(countriesJson))
            {
                return new List<CountryDataModel>();
            }

            return JsonSerializer.Deserialize<IEnumerable<CountryDataModel>>(countriesJson) ?? new List<CountryDataModel>();
        }

        private async Task SetCache(IEnumerable<CountryDataModel>? countries, CancellationToken cancellationToken)
        {
            await _cache.SetStringAsync(
                 ALL_COUNTRIES_KEY,
                 JsonSerializer.Serialize(countries),
                 new DistributedCacheEntryOptions
                 {
                     AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                 },
                 cancellationToken);
        }
    }
}
