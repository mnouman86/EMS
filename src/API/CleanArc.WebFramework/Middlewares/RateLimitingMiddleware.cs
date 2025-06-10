using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Interfaces.Services;
using CleanArc.Domain.Settings;
using CleanArc.Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.WebFramework.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRateLimiter _rateLimiter;
        private readonly IClientIdentifier _clientIdentifier;
        private readonly RateLimitSettings _settings;
        private readonly ILogger<RateLimitingMiddleware> _logger;
        private readonly IServiceProvider _serviceProvider;

        public RateLimitingMiddleware(
            RequestDelegate next,
            IRateLimiter rateLimiter,
            IClientIdentifier clientIdentifier,
            IOptions<RateLimitSettings> settings,
            ILogger<RateLimitingMiddleware> logger,
            IServiceProvider serviceProvider)
        {
            _next = next;
            _rateLimiter = rateLimiter;
            _clientIdentifier = clientIdentifier;
            _settings = settings.Value;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Skip rate limiting if disabled
            if (!_settings.Enabled)
            {
                await _next(context);
                return;
            }

            var clientId = _clientIdentifier.GetClientId(context);
            var endpoint = context.Request.Path;

            // Check if request is allowed
            var isAllowed = await _rateLimiter.IsRequestAllowedAsync(clientId, context.RequestAborted);

            if (!isAllowed)
            {
                await HandleRateLimitedRequest(context, clientId, endpoint);
                return;
            }

            // Optional: Check for suspicious activity
            await CheckForSuspiciousActivity(context, clientId);

            await _next(context);
        }

        private async Task HandleRateLimitedRequest(HttpContext context, string clientId, string endpoint)
        {
            _logger.LogWarning("Rate limit exceeded for client {ClientId} on {Endpoint}",
                clientId, endpoint);

            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            context.Response.Headers["Retry-After"] = _settings.GlobalWindowInSeconds.ToString();
            
            var errorResponse = OperationResult<bool>.FailureResult(
            $"Rate limit exceeded. Please try again in {_settings.GlobalWindowInSeconds} seconds.",
            StatusCodes.Status429TooManyRequests, ErrorCodes.RateLimitExceeded);

            await context.Response.WriteAsJsonAsync(errorResponse);
            //await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
        }

        private async Task CheckForSuspiciousActivity(HttpContext context, string clientId)
        {
            // Example: Flag clients with suspicious headers
            if (context.Request.Headers.ContainsKey("X-Suspicious-Header"))
            {
                if (_rateLimiter is AdaptiveRateLimiter adaptiveLimiter)
                {
                    adaptiveLimiter.FlagSuspiciousClient(clientId, "Suspicious header detected");
                    _logger.LogWarning("Flagged suspicious client {ClientId} for suspicious header", clientId);
                }
            }

            // Example: Flag failed auth attempts
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                if (_rateLimiter is AdaptiveRateLimiter adaptiveLimiter)
                {
                    adaptiveLimiter.FlagSuspiciousClient(clientId, "Failed authentication");
                    _logger.LogWarning("Flagged suspicious client {ClientId} for failed auth", clientId);
                }
            }

            await Task.CompletedTask;
        }
    }

    public static class RateLimitingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RateLimitingMiddleware>();
        }
    }
}
