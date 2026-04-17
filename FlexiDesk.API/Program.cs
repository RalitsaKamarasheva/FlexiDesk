using FlexiDesk.API.Mappers;
using FlexiDesk.Domain.Entities;
using FlexiDesk.Domain.Interfaces;
using FlexiDesk.Domain.Validators;
using FlexiDesk.Infrastructure.Factories;
using FlexiDesk.Infrastructure.Handlers;
using FlexiDesk.Infrastructure.Persistence;
using FlexiDesk.Infrastructure.Repositories;
using FlexiDesk.Infrastructure.Services;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
string connectionString = "";
#if DEBUG
connectionString = builder.Configuration.GetSection("DB:FlexiDesk:WindowsDev").Value;
#else
    connectionString = builder.Configuration.GetSection("DB:FlexiDesk:WindowsProd").Value;
#endif

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddDbContext<FlexiDeskContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IDbConnectionFactory<SqlConnection>, SqlConnectionFactory>();
builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IDomainEventHandler<Reservation>, AuditLogHandler>();
builder.Services.AddValidatorsFromAssemblyContaining<ReservationValidator>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
