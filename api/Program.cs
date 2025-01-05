﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using api.Data;
using api.Repositories;
var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("EmployeesDbContext") ?? throw new InvalidOperationException("Connection string 'EmployeesDbContext' not found.");

builder.Services.AddDbContext<EmployeesDbContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

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