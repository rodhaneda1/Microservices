using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.CrossCutting.DependecyInjector
{
    public static class LoggingServiceCollectionExtension
    {
        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("Microservices.Api");
            services.AddSingleton<ILogger>(logger);
            
            return services;
        }
    }
}