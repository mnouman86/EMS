using Azure;
using CleanArc.Application.Models.ApiResult;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Common;
using CleanArc.Domain.Common.Exceptions;
using CleanArc.SharedKernel.Extensions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; 
using System.IO.IsolatedStorage;

namespace CleanArc.WebFramework.Middlewares;

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next,
        IWebHostEnvironment env,
        ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _env = env;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException validationException)
        {
            //context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

            //var errors = new Dictionary<string, List<string>>();

            //foreach (var validationExceptionError in validationException.Errors)
            //{
            //    if(!errors.ContainsKey(validationExceptionError.PropertyName))
            //        errors.Add(validationExceptionError.PropertyName,new List<string>(){validationExceptionError.ErrorMessage});
            //    else
            //        errors[validationExceptionError.PropertyName].Add(validationExceptionError.ErrorMessage);

            //}

            //var apiResult = new ApiResult<IDictionary<string, List<string>>>(false, ApiResultStatusCode.EntityProcessError, errors, ApiResultStatusCode.EntityProcessError.ToDisplay());

            //context.Response.ContentType = "application/problem+json";
            //await context.Response.WriteAsJsonAsync(apiResult);
            // New implementation using OperationResult
            var errorMessages = validationException.Errors
                .Select(e => e.ErrorMessage)
                .ToList();
            var combinedMessage = string.Join(" | ", errorMessages);
            string errorCode = ErrorCodes.ValidationError;
            string errorMessage = ErrorMessages.GetMessage(errorCode);

            var result = OperationResult<object>.FailureResult(
                message: errorMessage,
                statusCode: StatusCodes.Status422UnprocessableEntity,
                errorCode: errorCode
            );

            //context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            //context.Response.ContentType = "application/problem+json";
            //await context.Response.WriteAsJsonAsync(result);
            await WriteResponse(context, result);

        }
        catch (EmailVerificationRequiredException)
        {
            var result = OperationResult<object>.FailureResult(
                statusCode: StatusCodes.Status403Forbidden,
                errorCode: ErrorCodes.EmailVerificationRequired
            );

            //context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //context.Response.ContentType = "application/problem+json";
            //await context.Response.WriteAsJsonAsync(result);
            await WriteResponse(context, result);
        }
        catch (UnauthorizedAccessException ex) when (ex is not EmailVerificationRequiredException)
        {
            var result = OperationResult<object>.FailureResult(
                statusCode: StatusCodes.Status403Forbidden,
                errorCode: ErrorCodes.AccessDenied
            );

            //context.Response.StatusCode = StatusCodes.Status403Forbidden;
            //context.Response.ContentType = "application/problem+json";
            //await context.Response.WriteAsJsonAsync(result);
            await WriteResponse(context, result);
        }
        //catch (UnauthorizedAccessException authException)
        //{
        //    _logger.LogWarning(authException, "Authorization failure");

        //    var statusCode = StatusCodes.Status403Forbidden;
        //    var result = OperationResult<object>.FailureResult(
        //        message: authException.Message, // "Email verification required"
        //        statusCode: statusCode
        //    );

        //    context.Response.StatusCode = statusCode;
        //    context.Response.ContentType = "application/problem+json";
        //    await context.Response.WriteAsJsonAsync(result);
        //}
        catch (Exception exception)
        {
            _logger.LogError(exception,exception.Message);
            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            //if (!_env.IsDevelopment())
            //{
            //    context.Response.ContentType = "application/problem+json";
            //    var response = new ApiResult(false,
            //        ApiResultStatusCode.ServerError, "Server Error");
            //    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //    await context.Response.WriteAsJsonAsync(response);
            //}
            // New implementation using OperationResult
            

            var result = OperationResult<object>.FailureResult(
                message: _env.IsDevelopment()
                    ? exception.ToString()
                    : exception.ToString(),
                    //: "An unexpected error occurred",
                statusCode: StatusCodes.Status500InternalServerError,
                errorCode: ErrorCodes.ServerError
            );

            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //context.Response.ContentType = "application/problem+json";
            //await context.Response.WriteAsJsonAsync(result);
            await WriteResponse(context, result);
            //await _next(context);
        }

    }
    private async Task WriteResponse(HttpContext context, OperationResult<object> result)
    {
        context.Response.StatusCode = result.StatusCode;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(result);
    }
}