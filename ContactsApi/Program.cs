using ContactsApi.Data;
using ContactsApi.Repositories;
using ContactsApi.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EntityFramework.DbContextScope.Interfaces;
using EntityFramework.DbContextScope;
using EllipticCurve.Utils;
using ContactsApi.Profiles.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ContactsApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiContext") ?? throw new InvalidOperationException("Connection string 'ContactsApiContext' not found.")));


builder.Services.AddAutoMapper(typeof(ContactMappingProfile));


// Register repositories and UnitOfWork
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ContactsApi.Data.IUnitOfWork, UnitOfWork>();

// Configure FluentValidation
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ContactValidator>());

// Register the ContactService
builder.Services.AddScoped<IContactService, ContactService>();

// Add Authorization services
builder.Services.AddAuthorization();

// Add Controllers
builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agrega el servicio de registro (ILogger<T>) a tus servicios
builder.Services.AddLogging();  // Agrega esta línea
builder.Services.AddScoped<IDbContextScopeFactory, DbContextScopeFactory>();
var app = builder.Build();

// Configure the HTTP pipeline
// Otros servicios y configuraciones

// Configure the HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature.Error;

        logger.LogError(exception, "Unhandled exception");

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Internal Server Error");
    });
});
app.UseStatusCodePagesWithReExecute("/error/{0}");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
