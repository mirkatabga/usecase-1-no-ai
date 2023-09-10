using Microsoft.Extensions.DependencyInjection;
using UseCaseOneNoAI.Application.Countries.Queries;

namespace UseCaseOneNoAI.Application
{
    public static class ConfigureServices
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(opt => opt.RegisterServicesFromAssemblyContaining<GetCountriesQuery>());
            services.AddAutoMapper(typeof(ConfigureServices).Assembly);
        }
    }
}
