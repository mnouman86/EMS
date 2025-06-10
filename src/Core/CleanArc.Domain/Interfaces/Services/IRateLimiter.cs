using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Interfaces.Services
{
    // Core/Interfaces/Services/IRateLimiter.cs
    public interface IRateLimiter
    {
        /// <summary>
        /// Checks if a request is allowed for the given client
        /// </summary>
        Task<bool> IsRequestAllowedAsync(string clientId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the base rate limit (requests per window)
        /// </summary>
        int GetBaseLimit();

        /// <summary>
        /// Gets the base window in seconds
        /// </summary>
        int GetBaseWindow();

        /// <summary>
        /// Resets the count for a specific client
        /// </summary>
        Task ResetClientAsync(string clientId, CancellationToken cancellationToken = default);
    }
}
