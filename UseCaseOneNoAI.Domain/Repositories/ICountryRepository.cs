using UseCaseOneNoAI.Domain.Entities;

namespace UseCaseOneNoAI.Domain.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<CountryEntity>> GetCountriesAsync(
            string? name = null,
            int? maxPopulation = null,
            bool? ascending = null,
            int? take = null,
            CancellationToken cancellationToken = new CancellationToken());
    }
}
