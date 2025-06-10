using Asp.Versioning;
using CleanArc.Domain.Interfaces.Services;
using CleanArc.Domain.Settings;
using CleanArc.Infrastructure.Persistence.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArc.WebFramework.ServiceConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddWebFrameworkServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.Configure<RateLimitSettings>(configuration.GetSection("RateLimit"));
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IClientIdentifier, ClientIdentifier>();

        // Register the base limiter first
        services.AddSingleton<InMemoryRateLimiter>();

        // Then register the adaptive limiter as the main IRateLimiter implementation
        services.AddSingleton<IRateLimiter>(provider =>
        {
            var baseLimiter = provider.GetRequiredService<InMemoryRateLimiter>();
            var logger = provider.GetRequiredService<ILogger<AdaptiveRateLimiter>>();
            return new AdaptiveRateLimiter(baseLimiter, logger);
        });

        return services;
    }
}