using UseCaseOneNoAI.Infrastructure.Clients;
using UseCaseOneNoAI.Infrastructure.Models;

namespace UseCaseOneNoAI.Tests.TestDoubles.Fakes
{
    internal class CountryClientFake : ICountryClient
    {
        async Task<IEnumerable<CountryDataModel>> ICountryClient.GetCountriesAsync(CancellationToken cancellationToken)
        {
            var countries = new List<CountryDataModel>
            {
                new CountryDataModel
                {
                    Name = new CountryDataModel.NameDataModel { Common = "Spain" },
                    Population = 47_420_000
                },
                new CountryDataModel
                {
                    Name= new CountryDataModel.NameDataModel { Common = "Afganistan" },
                    Population = 40_100_000
                },
                new CountryDataModel
                {
                    Name= new CountryDataModel.NameDataModel { Common = "Estonia" },
                    Population = 1_330_000
                },
                new CountryDataModel
                {
                    Name= new CountryDataModel.NameDataModel { Common = "United States" },
                    Population = 331_900_000
                },
                new CountryDataModel
                {
                    Name= new CountryDataModel.NameDataModel { Common = "Austria" },
                    Population = 8_950_000
                },
            };


            return await Task.FromResult(countries);
        }
    }
}
