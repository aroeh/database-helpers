using DatabaseHelpers.DataAccess;
using DatabaseHelpers.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Add services

// Add MongoDB Services for Logic Repo Layer, Data Layer, and Mongo Interface Helper
builder.Services.AddTransient<IRestuarantRepo, RestuarantRepo>();
builder.Services.AddTransient<IRestuarantData, RestuarantData>();
builder.Services.AddTransient<IMongoService, MongoService>();
// TODO: Add DB Helpers for Mongo, SQL, CosmosDb

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
