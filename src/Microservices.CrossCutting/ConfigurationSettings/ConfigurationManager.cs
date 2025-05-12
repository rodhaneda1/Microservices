using System;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.CrossCutting.ConfigurationSettings
{
    [ExcludeFromCodeCoverage]
    public static class ConfigurationManager
    {
        public static IServiceCollection AddConfigurationManager(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", true, true)
                .Build();

            return services;
        }
    }
}