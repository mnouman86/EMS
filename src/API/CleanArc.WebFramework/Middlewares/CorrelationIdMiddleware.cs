using Azure;
using CleanArc.Application.Models.ApiResult;
using CleanArc.SharedKernel.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using Serilog.Context;

namespace CleanArc.WebFramework.Middlewares;

public static class CorrelationIdMiddlewareExtensions
{
    public static IApplicationBuilder UseCorrelationIdMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CorrelationIdMiddleware>();
    }
}

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext context)
    {
        // Generate a unique correlation ID for the request
        var correlationId = Guid.NewGuid().ToString();

        // Set the correlation ID in the response headers
        context.Response.Headers.Add("X-Correlation-ID", correlationId);

        // Enrich the log context with the correlation ID
        LogContext.PushProperty("CorrelationId", correlationId);

        // Continue with the request pipeline
        await _next(context);
    }
}