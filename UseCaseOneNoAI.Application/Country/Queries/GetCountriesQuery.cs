using AutoMapper;
using MediatR;
using UseCaseOneNoAI.Application.Resources;
using UseCaseOneNoAI.Domain.Repositories;

namespace UseCaseOneNoAI.Application.Queries;

public record GetCountriesQuery(
    string? Name,
    int? MaxPopulation,
    bool? Ascending,
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
            request.MaxPopulation,
            request.Ascending,
            request.Take,
            cancellationToken);

        return _mapper.Map<IEnumerable<Country>>(countries);
    }
}
