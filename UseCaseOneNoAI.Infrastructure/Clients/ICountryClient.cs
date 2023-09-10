using UseCaseOneNoAI.Infrastructure.Models;

namespace UseCaseOneNoAI.Infrastructure.Clients
{
    internal interface ICountryClient
    {
        Task<IEnumerable<CountryDataModel>> GetCountriesAsync(CancellationToken cancellationToken);
    }
}
