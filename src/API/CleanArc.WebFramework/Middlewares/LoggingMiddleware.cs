using Azure;
using Castle.DynamicProxy;
using CleanArc.Application.Models.ApiResult;
using CleanArc.SharedKernel.Extensions;
using CleanArc.WebFramework.Interceptor;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using Serilog.Context;
using System.Security.Claims;
using System.Text;

namespace CleanArc.WebFramework.Middlewares;

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomLoggingHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        // Enrich the log context with the user ID
        //LogContext.PushProperty("UserId", GetUserIdFromContext(context));
        //LogContext.PushProperty("UserId", User.Identity.GetUserId());

        LogContext.PushProperty("UserId",context?.User?.Identity?.Name ?? "anonymous");
        string token = context?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        LogContext.PushProperty("Token", token); // Push the token to the log context
        // Log the request
        LogRequest(context?.Request);

        // Call the next middleware in the pipeline
        //using (LogContext.PushProperty("UserId", context?.User?.Identity?.Name ?? "anonymous"))
        //{
        //    await _next(context);
        //}
        await _next(context);

        // Log the response
        LogResponse(context.Response);
    }

    private async void LogRequest(HttpRequest request)
    {
        // Log request details as needed
        _logger.LogInformation("Request Method: {RequestMethod}, Path: {RequestPath}, Content-Type: {ContentType}",
            request.Method, request.Path, request.ContentType);

        // Log request body if present
        if (request.Body.CanRead && request.ContentLength > 0)
        {
            using (var reader = new StreamReader(request.Body, Encoding.UTF8))
            {
                var requestBody = await reader.ReadToEndAsync();
                _logger.LogInformation("Request Body: {RequestBody}", requestBody);
                request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            }
        }
    }

    private async void LogResponse(HttpResponse response)
    {
        // Log response details as needed
        _logger.LogInformation("Response StatusCode: {StatusCode}", response.StatusCode);

        // Log response body if present
        if (response.Body.CanRead && response.ContentLength > 0)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(response.Body, Encoding.UTF8))
            {
                var responseBody = await reader.ReadToEndAsync();
                _logger.LogInformation("Response Body: {ResponseBody}", responseBody);
                response.Body.Seek(0, SeekOrigin.Begin);
            }
        }
    }
    //private string GetUserIdFromContext(HttpContext context)
    //{
    //    if (User != null)
    //    { int chk = int.Parse(User.Identity.GetUserId()); }
    //    // Replace this with your logic to get the user ID from the authentication context.
    //    // For example, if you are using HttpContext:
    //    return context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
    //}
}
