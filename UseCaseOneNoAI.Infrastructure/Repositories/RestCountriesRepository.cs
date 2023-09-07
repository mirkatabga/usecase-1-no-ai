using AutoMapper;
using System.Net.Http.Json;
using System.Text.Json;
using UseCaseOneNoAI.Domain.Entities;
using UseCaseOneNoAI.Domain.Exceptions;
using UseCaseOneNoAI.Domain.Repositories;
using UseCaseOneNoAI.Infrastructure.Models;

namespace UseCaseOneNoAI.Infrastructure.Repositories
{
    internal class RestCountriesRepository : ICountryRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public RestCountriesRepository(
            IHttpClientFactory httpClientFactory,
            IMapper mapper)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(ICountryRepository));
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryEntity>> GetCountriesAsync(
            string? name = null,
            int? maxPopulation = null,
            bool? ascending = null,
            int? take = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var response = await _httpClient.GetAsync("/v3.1/all", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var reason = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new DomainException($"Could not retrive countries. Reason: {reason}");
            }

            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var countries = await response.Content.ReadFromJsonAsync<IEnumerable<CountryDataModel>>(
                jsonOptions, cancellationToken);

            return _mapper.Map<IEnumerable<CountryEntity>>(countries);
        }
    }
}
