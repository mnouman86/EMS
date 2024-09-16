using Azure;
using CleanArc.Application.Models.ApiResult;
using CleanArc.SharedKernel.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System.Text;

namespace CleanArc.WebFramework.Middlewares;

public static class LoggingMiddlewareSimpleExtensions
{
    public static IApplicationBuilder UseCustomLoggingSimpleHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddlewareSimple>();
    }
}

public class LoggingMiddlewareSimple
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddlewareSimple> _logger;

    public LoggingMiddlewareSimple(RequestDelegate next, ILogger<LoggingMiddlewareSimple> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }


    public async Task Invoke(HttpContext context)
    {
        // Log the request
        LogRequest(context.Request);

        // Call the next middleware in the pipeline
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
                var responseBody =await reader.ReadToEndAsync();
                _logger.LogInformation("Response Body: {ResponseBody}", responseBody);
                response.Body.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}