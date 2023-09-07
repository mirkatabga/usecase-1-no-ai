using UseCaseOneNoAI.Domain.Entities;

namespace UseCaseOneNoAI.Domain.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<CountryEntity>> GetCountriesAsync(
            string? name = null,
            double? maxPopulationInMil = null,
            bool? ascending = null,
            int? take = null,
            CancellationToken cancellationToken = new CancellationToken());
    }
}
