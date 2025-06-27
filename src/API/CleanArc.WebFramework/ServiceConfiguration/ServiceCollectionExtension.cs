using Asp.Versioning;
using CleanArc.Domain.Interfaces.Services;
using CleanArc.Domain.Settings;
using CleanArc.Infrastructure.Persistence.Services;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.RateLimiting;

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
        services.Configure<EmailVerificationSettings>(configuration.GetSection("EmailVerificationSettings"));
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
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
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter("global", _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 100,
                    Window = TimeSpan.FromMinutes(1),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 0
                }));
        });
        return services;
    }
}