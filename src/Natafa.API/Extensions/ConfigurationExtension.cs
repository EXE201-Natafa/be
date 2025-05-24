using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using Natafa.Api.Models.Configurations;
using Natafa.Api.Services.Implements;
using Natafa.Api.Services.Interfaces;
using Natafa.Domain.Entities;
using Natafa.Repository.Implement;
using Natafa.Repository.Interfaces;

namespace Natafa.Api.Extensions
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<NatafaDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DbConnection"),
                new MySqlServerVersion(new Version(8, 0, 37))));

            service.Configure<MailConfiguration>(configuration.GetSection("MailConfiguration"));
            service.Configure<AuthenticationConfiguration>(configuration.GetSection("AuthenticationConfiguration"));
            service.Configure<VnPayConfig>(configuration.GetSection("VnPayConfig"));
            service.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            return service;
        }
    }
}
