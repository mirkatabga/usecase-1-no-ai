using AutoMapper;
using UseCaseOneNoAI.Domain.ValueObjects;
using static UseCaseOneNoAI.Infrastructure.Models.CountryDataModel;

namespace UseCaseOneNoAI.Infrastructure.Mapping
{
    internal class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<CurrencyDataModel, Currency>().ReverseMap();
        }
    }
}
