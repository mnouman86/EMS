using CleanArc.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Services
{
    // Infrastructure/Services/AdaptiveRateLimiter.cs
    public class AdaptiveRateLimiter : IRateLimiter
    {
        private readonly IRateLimiter _baseLimiter;
        private readonly ConcurrentDictionary<string, int> _penaltyBox = new();
        private readonly ILogger<AdaptiveRateLimiter> _logger;

        public AdaptiveRateLimiter(IRateLimiter baseLimiter, ILogger<AdaptiveRateLimiter> logger)
        {
            _baseLimiter = baseLimiter;
            _logger = logger;
        }

        public async Task<bool> IsRequestAllowedAsync(string clientId, CancellationToken cancellationToken = default)
        {
            if (_penaltyBox.TryGetValue(clientId, out var penaltyLevel))
            {
                // Calculate reduced limit (minimum 1)
                var reducedLimit = Math.Max(1, _baseLimiter.GetBaseLimit() / (penaltyLevel + 1));
                var reducedWindow = Math.Max(5, _baseLimiter.GetBaseWindow() / (penaltyLevel + 1));

                _logger.LogDebug("Applying penalty level {Level} to client {ClientId} - Limit: {Limit}/{Window}s",
                    penaltyLevel, clientId, reducedLimit, reducedWindow);

                // Create temporary strict limiter
                var tempLimiter = new TemporaryRateLimiter(reducedLimit, reducedWindow);
                var allowed = await tempLimiter.IsRequestAllowedAsync(clientId, cancellationToken);

                if (!allowed)
                {
                    _penaltyBox[clientId] = penaltyLevel + 1;
                    _logger.LogWarning("Increased penalty for client {ClientId} to level {Level}",
                        clientId, penaltyLevel + 1);
                }

                return allowed;
            }

            return await _baseLimiter.IsRequestAllowedAsync(clientId, cancellationToken);
        }

        public int GetBaseLimit() => _baseLimiter.GetBaseLimit();
        public int GetBaseWindow() => _baseLimiter.GetBaseWindow();

        public async Task ResetClientAsync(string clientId, CancellationToken cancellationToken = default)
        {
            _penaltyBox.TryRemove(clientId, out _);
            await _baseLimiter.ResetClientAsync(clientId, cancellationToken);
        }

        public void FlagSuspiciousClient(string clientId, string reason)
        {
            var newLevel = _penaltyBox.AddOrUpdate(clientId,
                addValue: 1,
                updateValueFactory: (_, existing) => existing + 1);

            _logger.LogWarning("Flagged suspicious client {ClientId} (Reason: {Reason}) - New penalty level: {Level}",
                clientId, reason, newLevel);
        }

        private class TemporaryRateLimiter : IRateLimiter
        {
            private readonly int _limit;
            private readonly int _windowSeconds;
            private readonly ConcurrentDictionary<string, (int Count, DateTime Expiry)> _counts = new();

            public TemporaryRateLimiter(int limit, int windowSeconds)
            {
                _limit = limit;
                _windowSeconds = windowSeconds;
            }

            public Task<bool> IsRequestAllowedAsync(string clientId, CancellationToken cancellationToken = default)
            {
                var now = DateTime.UtcNow;

                var entry = _counts.AddOrUpdate(clientId,
                    _ => (1, now.AddSeconds(_windowSeconds)),
                    (_, existing) =>
                    {
                        if (existing.Expiry < now)
                        {
                            return (1, now.AddSeconds(_windowSeconds));
                        }
                        return (existing.Count + 1, existing.Expiry);
                    });

                return Task.FromResult(entry.Count <= _limit);
            }

            public int GetBaseLimit() => _limit;
            public int GetBaseWindow() => _windowSeconds;

            public Task ResetClientAsync(string clientId, CancellationToken cancellationToken = default)
            {
                _counts.TryRemove(clientId, out _);
                return Task.CompletedTask;
            }
        }
    }
}
