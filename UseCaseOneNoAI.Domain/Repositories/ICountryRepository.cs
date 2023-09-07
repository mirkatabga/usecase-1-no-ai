using UseCaseOneNoAI.Domain.Entities;
using UseCaseOneNoAI.Domain.Enums;

namespace UseCaseOneNoAI.Domain.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<CountryEntity>> GetCountriesAsync(
            string? name = null,
            double? maxPopulationInMil = null,
            SortType sortType = SortType.None,
            int? take = null,
            CancellationToken cancellationToken = new CancellationToken());
    }
}
