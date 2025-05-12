using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microservices.Api.Middlewares;
using Microservices.CrossCutting.Middleware;
using Microservices.CrossCutting.AutoMapper;
using Microservices.CrossCutting.DependecyInjector;
using Microservices.CrossCutting.DependencyInjector;
using Microservices.CrossCutting.ConfigurationSettings;

namespace Microservices.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Microservices",
                    Description = "Microservices API REST criada com o ASP.NET Core",
                    Version = "0.0.1"
                });

                c.ResolveConflictingActions(api => api.First());
            });

            services.AddLogger(Configuration);
            services.AddMediator();
            services.AddRepository();
            services.AddControllers();
            services.SetupAutoMapper();
            services.AddHealthChecks();
            services.AddConfigurationManager();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<LogMiddleware>();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandlerMiddleware(env);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json",
                        "Microservices - Version 0.0.1");
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    ResponseWriter = (context, health) =>
                    {
                        context.Response.ContentType = "application/json";
                        var options = new JsonWriterOptions { Indented = true };
                        using var memoryStream = new MemoryStream();
                        using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
                        {
                            jsonWriter.WriteStartObject();

                            foreach (var healthEntry in health.Entries)
                            {
                                jsonWriter.WriteString("status", healthEntry.Value.Status == HealthStatus.Healthy ? "UP" : "DOWN");
                            }

                            jsonWriter.WriteEndObject();
                        }

                        return context.Response.WriteAsync(Encoding.UTF8.GetString(memoryStream.ToArray()));
                    }
                });
            });
        }
    }
}