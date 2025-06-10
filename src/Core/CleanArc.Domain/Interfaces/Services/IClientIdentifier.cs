using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Interfaces.Services
{
    public interface IClientIdentifier
    {
        string GetClientId(HttpContext context);
    }

    public class ClientIdentifier : IClientIdentifier
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientIdentifier(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetClientId(HttpContext context)
        {
            // Try to get client ID from different sources in order of priority
            var clientId = GetClientIdFromAuthenticatedUser(context)
                        ?? GetClientIdFromApiKey(context)
                        ?? GetClientIdFromIpAddress(context);

            return clientId ?? "anonymous";
        }

        private string GetClientIdFromAuthenticatedUser(HttpContext context)
        {
            // If user is authenticated, use their ID
            if (context.User?.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "sub")
                              ?? context.User.Claims.FirstOrDefault(c => c.Type == "client_id");

                return userIdClaim?.Value;
            }

            return null;
        }

        private string GetClientIdFromApiKey(HttpContext context)
        {
            // Check for API key in headers or query string
            if (context.Request.Headers.TryGetValue("X-API-Key", out var apiKey) && !string.IsNullOrEmpty(apiKey))
            {
                return $"api-key:{apiKey}";
            }

            if (context.Request.Query.TryGetValue("api_key", out var queryApiKey) && !string.IsNullOrEmpty(queryApiKey))
            {
                return $"api-key:{queryApiKey}";
            }

            return null;
        }

        private string GetClientIdFromIpAddress(HttpContext context)
        {
            // Get IP address, considering proxies and load balancers
            var ipAddress = context.Connection.RemoteIpAddress;

            // Handle forwarded headers (if behind proxy)
            if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                var ips = forwardedFor.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (ips.Length > 0 && IPAddress.TryParse(ips[0], out var forwardedIp))
                {
                    ipAddress = forwardedIp;
                }
            }

            return ipAddress?.ToString();
        }
    }
}
