using Microsoft.EntityFrameworkCore;
using Natafa.Api.Extensions;
using Natafa.Api.Models.Configurations;
using Natafa.Domain.Entities;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddConfiguration(builder.Configuration)
                .AddService()
                .AddAuthenticationConfig()
                .AddCustomController();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5174") // Your frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Important for cookies/auth
    });
});

// Load assemblies for auto mapper to scan
var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic)
                    .ToArray();

builder.Services.AddAutoMapper(assemblies);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Natafa.API v1");
//    });
//}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Natafa.API v1");
});

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
