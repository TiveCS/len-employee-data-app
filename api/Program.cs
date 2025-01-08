using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using api.Data;
using api.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add logging at the start to see the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Using connection string: {connectionString}");

builder.Services.AddDbContext<EmployeesDbContext>(options =>
{
	try
	{
		options.UseMySql(
			connectionString,
			ServerVersion.AutoDetect(connectionString),
			mysqlOptions =>
			{
				mysqlOptions.EnableRetryOnFailure(
					maxRetryCount: 5,
					maxRetryDelay: TimeSpan.FromSeconds(30),
					errorNumbersToAdd: null
				);
			}
		);
	}
	catch (Exception ex)
	{
		Console.WriteLine($"Error configuring MySQL: {ex.Message}");
		throw;
	}
});

// Add services to the container.

builder.Services.AddScoped<EmployeesRepository>();
builder.Services.AddScoped<EmploymentLevelsRepository>();
builder.Services.AddScoped<WorkUnitsRepository>();
builder.Services.AddScoped<EmployeesWorkHistoriesRepository>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
