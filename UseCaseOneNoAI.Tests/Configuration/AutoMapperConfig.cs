using AutoMapper;

namespace UseCaseOneNoAI.Tests.Configuration
{
    internal static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var infrastructureAssembly = typeof(Infrastructure.ConfigureServices).Assembly;
            var applicationAssembly = typeof(UseCaseOneNoAI.Application.Countries.Queries.GetCountriesQuery).Assembly;

            var mapperConfig = new MapperConfiguration(
                opt => opt.AddMaps(infrastructureAssembly, applicationAssembly));

            return mapperConfig.CreateMapper();
        }
    }
}
