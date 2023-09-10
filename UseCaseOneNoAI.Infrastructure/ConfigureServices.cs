using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UseCaseOneNoAI.Domain.Repositories;
using UseCaseOneNoAI.Infrastructure.Clients;
using UseCaseOneNoAI.Infrastructure.Repositories;

namespace UseCaseOneNoAI.Infrastructure
{
    public static class ConfigureServices
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ICountryClient, RestCountryClient>(opt =>
            {
                var baseUrl = configuration["Infrastructure:CountriesProvider:BaseUrl"]!;
                opt.BaseAddress = new Uri(baseUrl);
            });

            services.AddTransient<ICountryClient, RestCountryClient>();
            services.AddTransient<ICountryRepository, RestCountryRepository>();
            services.AddAutoMapper(typeof(ConfigureServices).Assembly);
        }
    }
}
