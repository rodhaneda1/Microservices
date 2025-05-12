using Microsoft.Extensions.DependencyInjection;

namespace Microservices.CrossCutting.DependencyInjector
{
    public static class RepositoryServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            return services;
        }
    }
}