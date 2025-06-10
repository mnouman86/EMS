using CleanArc.Domain.Interfaces.Services;
using CleanArc.Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Services
{
    public class InMemoryRateLimiter : IRateLimiter
    {
        private readonly ConcurrentDictionary<string, ClientRateLimit> _clientLimits = new();
        private readonly ILogger<InMemoryRateLimiter> _logger;
        private readonly Timer _cleanupTimer;
        private readonly RateLimitSettings _settings;

        public InMemoryRateLimiter(IOptions<RateLimitSettings> settings, ILogger<InMemoryRateLimiter> logger)
        {
            _settings = settings.Value;
            _logger = logger;
            _cleanupTimer = new Timer(_ => CleanupExpiredEntries(), null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        }

        public Task<bool> IsRequestAllowedAsync(string clientId, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;

            var clientLimit = _clientLimits.AddOrUpdate(clientId,
                _ => new ClientRateLimit
                {
                    Count = 1,
                    Expiry = now.AddSeconds(_settings.GlobalWindowInSeconds)
                },
                (_, existing) =>
                {
                    if (existing.Expiry < now)
                    {
                        return new ClientRateLimit
                        {
                            Count = 1,
                            Expiry = now.AddSeconds(_settings.GlobalWindowInSeconds)
                        };
                    }
                    return new ClientRateLimit
                    {
                        Count = existing.Count + 1,
                        Expiry = existing.Expiry
                    };
                });

            if (clientLimit.Count > _settings.GlobalLimit)
            {
                _logger.LogWarning("Rate limit exceeded for client {ClientId} - {Count}/{Limit}",
                    clientId, clientLimit.Count, _settings.GlobalLimit);
            }

            return Task.FromResult(clientLimit.Count <= _settings.GlobalLimit);
        }

        public int GetBaseLimit() => _settings.GlobalLimit;
        public int GetBaseWindow() => _settings.GlobalWindowInSeconds;

        public Task ResetClientAsync(string clientId, CancellationToken cancellationToken = default)
        {
            _clientLimits.TryRemove(clientId, out _);
            return Task.CompletedTask;
        }

        private void CleanupExpiredEntries()
        {
            var now = DateTime.UtcNow;
            var expiredKeys = _clientLimits
                .Where(kvp => kvp.Value.Expiry < now)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var key in expiredKeys)
            {
                _clientLimits.TryRemove(key, out _);
            }

            if (expiredKeys.Count > 0)
            {
                _logger.LogDebug("Cleaned up {Count} expired rate limit entries", expiredKeys.Count);
            }
        }

        private class ClientRateLimit
        {
            public int Count { get; set; }
            public DateTime Expiry { get; set; }
        }
    }
}
