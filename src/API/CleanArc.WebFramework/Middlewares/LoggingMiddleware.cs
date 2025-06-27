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
    private readonly bool _isLoggingEnabled=false;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        if (!_isLoggingEnabled)
        {
            await _next(context);
            return;
        }

        LogContext.PushProperty("UserId", context?.User?.Identity?.Name ?? "anonymous");
        string token = context?.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        LogContext.PushProperty("Token", token);

        await LogRequest(context.Request);

        // To capture the response body, we need to replace the original stream
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context); // Proceed down the pipeline

        await LogResponse(context.Response);

        // Copy the response body back to the original stream
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
    }

    private async Task LogRequest(HttpRequest request)
    {
        _logger.LogInformation("Request Method: {RequestMethod}, Path: {RequestPath}, Content-Type: {ContentType}",
            request.Method, request.Path, request.ContentType);

        request.EnableBuffering(); // Allow re-reading the request body
        request.Body.Position = 0;

        using var reader = new StreamReader(request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        string body = await reader.ReadToEndAsync();

        request.Body.Position = 0; // Rewind for next middleware
        _logger.LogInformation("Request Body: {RequestBody}", body);
    }

    private async Task LogResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);

        using var reader = new StreamReader(response.Body);
        string body = await reader.ReadToEndAsync();

        _logger.LogInformation("Response StatusCode: {StatusCode}", response.StatusCode);
        _logger.LogInformation("Response Body: {ResponseBody}", body);

        response.Body.Seek(0, SeekOrigin.Begin); // Reset for copying back
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
