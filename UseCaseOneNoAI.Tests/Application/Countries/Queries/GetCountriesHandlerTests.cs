using AutoMapper;
using UseCaseOneNoAI.Application.Countries.Queries;
using UseCaseOneNoAI.Domain.Repositories;
using UseCaseOneNoAI.Infrastructure.Clients;
using UseCaseOneNoAI.Infrastructure.Repositories;
using UseCaseOneNoAI.Tests.Configuration;
using UseCaseOneNoAI.Tests.TestDoubles.Fakes;

namespace UseCaseOneNoAI.Tests.Application.Countries.Queries;

public class GetCountriesHandlerTests
{
    private readonly CancellationToken _cancellationToken = new();
    private readonly ICountryClient _client = new CountryClientFake();
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper = AutoMapperConfig.Initialize();

    public GetCountriesHandlerTests()
    {
        _countryRepository = new RestCountryRepository(_client, _mapper);
    }

    [Theory(DisplayName =
    "WHEN searching by partial name " +
    "THEN returns countries containing the search term")]
    [InlineData("Spain", 1)]
    [InlineData("st", 4)]
    [InlineData("St", 4)]
    public async Task Handle_PartialName_ReturnsCOuntriesContainingSearchTerm(string searchTerm, int expectedCount)
    {
        //Arrange
        var handler = new GetCountriesQueryHandler(_countryRepository, _mapper);

        var request = new GetCountriesQuery(
            Name: searchTerm,
            MaxPopulationInMil: null,
            SortType: null,
            Take: null);

        //Act
        var countries = await handler.Handle(request, _cancellationToken);

        //Assert
        countries
            .Should()
            .HaveCount(expectedCount);
    }

    [Theory(DisplayName =
    "WHEN searching with population limit " +
    "THEN returns countries with max population bellow the limit")]
    [InlineData(2.1, 1)]
    [InlineData(40.1, 3)]
    [InlineData(400, 5)]
    public async Task Handle_MaxPopulationInMil_ReturnsCountriesBellowTheLimit(double maxPopulationInMil, int expectedCount)
    {
        //Arrange
        var handler = new GetCountriesQueryHandler(_countryRepository, _mapper);

        var request = new GetCountriesQuery(
            Name: null,
            MaxPopulationInMil: maxPopulationInMil,
            SortType: null,
            Take: null);

        //Act
        var countries = await handler.Handle(request, _cancellationToken);

        //Assert
        countries
            .Should()
            .HaveCount(expectedCount);
    }


    [Theory(DisplayName =
    "WHEN sort is requested " +
    "THEN countries are sorted")]
    [InlineData("ascending", "Afganistan", "United States")]
    [InlineData("Descending", "United States", "Afganistan")]
    public async Task Handle_SortRequested_CountriesAreSorted(string sortDirection, string firstName, string lastName)
    {
        //Arrange
        var handler = new GetCountriesQueryHandler(_countryRepository, _mapper);

        var request = new GetCountriesQuery(
            Name: null,
            MaxPopulationInMil: null,
            SortType: sortDirection,
            Take: null);

        //Act
        var countries = await handler.Handle(request, _cancellationToken);

        //Assert
        countries.First().Name
            .Should()
            .Be(firstName);

        countries.Last().Name
            .Should()
            .Be(lastName);
    }


    [Fact(DisplayName =
    "WHEN take is set " +
    "THEN only expected count of countries returned")]
    public async Task Handle_TakeSet_ReturnsExpectedCountOfCountries()
    {
        //Arrange
        var take = 2;
        var handler = new GetCountriesQueryHandler(_countryRepository, _mapper);

        var request = new GetCountriesQuery(
            Name: null,
            MaxPopulationInMil: null,
            SortType: null,
            Take: take);

        //Act
        var countries = await handler.Handle(request, _cancellationToken);

        //Assert
        countries
            .Should()
            .HaveCount(take);
    }


    [Fact(DisplayName =
    "WHEN combined filters used " +
    "THEN returns expected count and order")]
    public async Task Handle_CombinedFilters_ReturnsExpected()
    {
        var handler = new GetCountriesQueryHandler(_countryRepository, _mapper);

        var request = new GetCountriesQuery(
            Name: "sT",
            MaxPopulationInMil: 50,
            SortType: "descending",
            Take: 2);

        //Act
        var countries = await handler.Handle(request, _cancellationToken);

        //Assert
        countries
            .Should()
            .HaveCount(2);

        countries.First().Name
            .Should()
            .Be("Estonia");

        countries.Last().Name
            .Should()
            .Be("Austria");
    }
}