using AutoMapper;
using UseCaseOneNoAI.Domain.Entities;
using UseCaseOneNoAI.Domain.Repositories;
using UseCaseOneNoAI.Infrastructure.Clients;
using UseCaseOneNoAI.Infrastructure.Models;

namespace UseCaseOneNoAI.Infrastructure.Repositories
{
    internal class RestCountriesRepository : ICountryRepository
    {
        private readonly ICountryClient _client;
        private readonly IMapper _mapper;

        public RestCountriesRepository(
            ICountryClient countryClient,
            IMapper mapper)
        {
            _client = countryClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryEntity>> GetCountriesAsync(
            string? name = null,
            int? maxPopulation = null,
            bool? ascending = null,
            int? take = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var countries = await _client.GetCountries(name, cancellationToken);
            var filtered = FilterByName(countries, name);

            return _mapper.Map<IEnumerable<CountryEntity>>(filtered);
        }

        private static IEnumerable<CountryDataModel> FilterByName(IEnumerable<CountryDataModel> countries, string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return countries;
            }

            return countries
                .Where(c => c.Name?.Common?.ToLowerInvariant()?.Contains(name.ToLowerInvariant()) == true);
        }
    }
}
