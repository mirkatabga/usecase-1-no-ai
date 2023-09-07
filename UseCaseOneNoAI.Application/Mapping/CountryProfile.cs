using AutoMapper;
using UseCaseOneNoAI.Domain.Entities;
using UseCaseOneNoAI.Application.Resources;

namespace UseCaseOneNoAI.Application.Mapping
{
    internal class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryEntity, Country>().ReverseMap();
        }
    }
}
