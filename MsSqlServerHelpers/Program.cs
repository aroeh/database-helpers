using MsSqlServerHelpers.DataAccess;
using MsSqlServerHelpers.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRestuarantRepo, RestuarantRepo>();
builder.Services.AddTransient<IRestuarantData, RestuarantData>();
builder.Services.AddTransient<ISqlHelperService, SqlHelperService>();
builder.Services.AddTransient<ISqlRefHelpers, SqlRefHelpers>();

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
