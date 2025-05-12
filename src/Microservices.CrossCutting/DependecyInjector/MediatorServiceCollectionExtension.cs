using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microservices.CrossCutting.DependencyInjector
{
    public static class MediatorServiceCollectionExtension
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("Microservices.Application");
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });

            return services;
        }
    }
}