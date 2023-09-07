using MediatR;
using UseCaseOneNoAI.Application;
using UseCaseOneNoAI.Application.Queries;
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

app.MapGet("/api/v1/countries", async (string? name, double? maxPopulationInMil, bool? ascending, int? take) =>
{
    var sender = app.Services.GetRequiredService<ISender>();
    var query = new GetCountriesQuery(name, maxPopulationInMil, ascending, take);
    var countries = await sender.Send(query);

    return countries;

})
.WithName("GetCountries");

app.Run();