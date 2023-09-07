using AutoMapper;
using UseCaseOneNoAI.Domain.Entities;
using UseCaseOneNoAI.Domain.Enums;
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
            double? maxPopulationInMil = null,
            SortType sortType = SortType.None,
            int? take = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var countries = await _client.GetCountries(name, cancellationToken);
            countries = FilterByName(countries, name);
            countries = FilterByPopulation(countries, maxPopulationInMil);
            countries = OrderByName(countries, sortType);

            return _mapper.Map<IEnumerable<CountryEntity>>(countries);
        }

        private IEnumerable<CountryDataModel> OrderByName(IEnumerable<CountryDataModel> countries, SortType sortType)
        {
            return sortType switch
            {
                SortType.None => countries,
                SortType.Ascending => countries.OrderBy(c => c.Name?.Common),
                SortType.Descending => countries.OrderByDescending(c => c.Name?.Common),
                _ => throw new NotImplementedException(),
            };
        }

        private IEnumerable<CountryDataModel> FilterByPopulation(IEnumerable<CountryDataModel> countries, double? maxPopulationInMil)
        {
            if (!maxPopulationInMil.HasValue)
            {
                return countries;
            }

            return countries
                .Where(c => c.Population <= maxPopulationInMil * 1_000_000);
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
