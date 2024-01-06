using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContactsApi.Data;
using ContactsApi.Controllers;
using ContactsApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ContactsApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsApiContext") ?? throw new InvalidOperationException("Connection string 'ContactsApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContactService, ContactService>();
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


