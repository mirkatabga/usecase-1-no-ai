using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;
using UseCaseOneNoAI.Application.Resources;
using UseCaseOneNoAI.Domain.Enums;
using UseCaseOneNoAI.Domain.Repositories;

namespace UseCaseOneNoAI.Application.Queries;

public record GetCountriesQuery(
    string? Name,
    double? MaxPopulationInMil,
    string? SortType,
    int? Take) : IRequest<IEnumerable<Country>>;

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IEnumerable<Country>>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public GetCountriesQueryHandler(
        ICountryRepository countryRepository,
        IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Country>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await _countryRepository.GetCountriesAsync(
            request.Name,
            request.MaxPopulationInMil,
            ParseSortType(request.SortType),
            request.Take,
            cancellationToken);

        return _mapper.Map<IEnumerable<Country>>(countries);
    }

    private static SortType ParseSortType(string? sortTypeValue)
    {
        if (string.IsNullOrWhiteSpace(sortTypeValue))
        {
            return SortType.None;
        }

        if (!Enum.TryParse(sortTypeValue, true, out SortType sortType))
        {
            throw new ValidationException($"Invalid value for {nameof(sortType)}");
        }

        return sortType;
    }
}
