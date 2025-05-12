using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microservices.Domain.Exceptions;

namespace Microservices.CrossCutting.Middleware
{
    public static class ExceptionHandler
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            return app.UseExceptionHandler(handler => handler.Run(async context =>
            {
                var _exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                
                if (_exception == default)
                {
                    return;
                }

                context.Response.ContentType = MediaTypeNames.Application.Json;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,

                };

                switch (((DomainException)_exception).Status)
                {
                    case HttpStatusCode.BadRequest:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(_exception.Data["response"],
                            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                        break;

                    case HttpStatusCode.NotFound:
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.Response.WriteAsync(JsonSerializer.Serialize(_exception.Data["response"],
                            new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                        break;

                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        var _error = new 
                        { 
                            Code = ((int)((DomainException)_exception).Status), 
                            Title = ((DomainException)_exception).Status.ToString(), 
                            Detail = env.IsProduction() ? string.Empty : _exception.Message 
                        };
                        await context.Response.WriteAsync(JsonSerializer.Serialize(_error, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
                        break;
                }
            }));
        }
    }
}
