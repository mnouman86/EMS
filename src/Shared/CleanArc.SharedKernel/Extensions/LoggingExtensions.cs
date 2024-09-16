using Microsoft.Extensions.Logging; 
using Newtonsoft.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Serilog.Context;
using System.Net.Http;
using Microsoft.AspNetCore.Http;


namespace CleanArc.SharedKernel.Extensions;

public static class LoggingExtensions
{
    public static IDisposable LogMethodEntryExit<T>(this ILogger<T> logger, HttpContext context=null, object parameter = null, [CallerMemberName] string memberName = "")
    {
        string _namespace = typeof(T).Namespace;
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        LogContext.PushProperty("UserId", context?.User?.Identity?.Name ?? "anonymous");
        LogContext.PushProperty("Token", token); // Push the token to the log context


        LogMethodEntry(logger, parameter,_namespace, memberName);

        return new MethodEntryExitLogger(logger,_namespace, memberName);
    }

    private static void LogMethodEntry<T>(ILogger<T> logger, object parameter,string _namespace, string methodName)
    {
        if (parameter != null)
        {
            var serializedParameter = JsonConvert.SerializeObject(parameter);
            logger.LogInformation("Entering {Namespace}, {MethodName} with parameter: {Parameter}", _namespace, methodName, serializedParameter);
        }
        else
        {
            logger.LogInformation("Entering {Namespace}, {MethodName}", _namespace, methodName);
        }
    }

    public class MethodEntryExitLogger : IDisposable
    {
        private readonly ILogger _logger;
        private readonly string _methodName;
        private readonly string _namespace;
        private object _response;

        public MethodEntryExitLogger(ILogger logger,string _namespace, string methodName)
        {
            _logger = logger;
            _methodName = methodName;
            _namespace = this._namespace;
        }

        public void SetResponse(object response)
        {
            _response = response;
        }

        public void Dispose()
        {
            if (_response != null)
            {
                var serializedResponse = JsonConvert.SerializeObject(_response);
                _logger.LogInformation("Exiting {Namespace}, {MethodName} with response: {Response}",_namespace, _methodName, serializedResponse);
            }
            else
            {
                _logger.LogInformation("Exiting {Namespace}, {MethodName}",_namespace, _methodName);
            }
        }
    }
}

