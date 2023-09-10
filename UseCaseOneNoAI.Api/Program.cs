using MediatR;
using UseCaseOneNoAI.Application;
using UseCaseOneNoAI.Application.Countries.Queries;
using UseCaseOneNoAI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/v1/countries", async (string? name, double? maxPopulationInMil, string? sortType, int? take) =>
{
    var sender = app.Services.GetRequiredService<ISender>();
    var query = new GetCountriesQuery(name, maxPopulationInMil, sortType, take);
    var countries = await sender.Send(query);

    return countries;

})
.WithName("GetCountries");

app.Run();