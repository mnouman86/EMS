using Castle.DynamicProxy;
using CleanArc.WebFramework.Middlewares;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging; using CleanArc.Domain.Common;
using System;
using System.Reflection;

namespace CleanArc.WebFramework.Interceptor
{
    public class LoggingInterceptor : Castle.DynamicProxy.IInterceptor
    {
        private readonly ILogger<LoggingInterceptor> _logger;
        private readonly ICustomHttpContext _context;


        public LoggingInterceptor(ILogger<LoggingInterceptor> logger, ICustomHttpContext context)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public LoggingInterceptor(ILogger<LoggingMiddleware> logger, ICustomHttpContext context)
        {
            Logger = logger;
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public ILogger<LoggingMiddleware> Logger { get; }

        public void Intercept(IInvocation invocation)
        {
            // Log method entry
            _logger.LogInformation("Entering method {MethodName}", invocation.Method.Name);

            try
            {
                // Proceed with the original method invocation
                invocation.Proceed();

                // Log method exit
                _logger.LogInformation("Exiting method {MethodName}", invocation.Method.Name);
            }
            catch (Exception ex)
            {
                // Log exception if it occurs
                _logger.LogError(ex, "Exception in method {MethodName}", invocation.Method.Name);

                // Rethrow the exception
                throw;
            }
        }
    }
}
