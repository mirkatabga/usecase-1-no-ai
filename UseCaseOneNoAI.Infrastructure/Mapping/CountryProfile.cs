using AutoMapper;
using UseCaseOneNoAI.Domain.Entities;
using UseCaseOneNoAI.Domain.ValueObjects;
using UseCaseOneNoAI.Infrastructure.Extensions;
using UseCaseOneNoAI.Infrastructure.Models;

namespace UseCaseOneNoAI.Infrastructure.Mapping
{
    internal class CountryProfile : Profile
    {
        public CountryProfile()
        {
            IValueConverter<IEnumerable<double>, Coordinates?> coordinatesConverted = new CoordinatesValueConverter();

            CreateMap<CountryDataModel, CountryEntity>()

                .ForCtorParam(nameof(CountryEntity.Name).ToCamelCase(), opt =>
                {
                    opt.MapFrom(model => model.Name!.Common);
                })

                .ForCtorParam(nameof(CountryEntity.CountryCodeTwoLetters).ToCamelCase(),
                    opt => opt.MapFrom(model => model.CCA2))

                .ForCtorParam(nameof(CountryEntity.CountryCodeThreeLetters).ToCamelCase(),
                    opt => opt.MapFrom(model => model.CCA3))

                .ForMember(entity => entity.Currency, opt =>
                {
                    opt.PreCondition(model => model.Currencies?.Any() == true);
                    opt.MapFrom(model => model.Currencies!.First().Value);
                })

                .ForMember(entity => entity.Coordinates,
                    opt => opt.ConvertUsing(coordinatesConverted, nameof(CountryDataModel.LatLng)))

                .ForMember(entity => entity.Capital, opt =>
                {
                    opt.PreCondition(model => model.Capital?.Any() == true);
                    opt.MapFrom(model => model.Capital!.First());
                });
        }
    }
}
