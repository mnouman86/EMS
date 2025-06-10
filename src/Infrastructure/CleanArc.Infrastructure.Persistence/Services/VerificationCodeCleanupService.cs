using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Services
{
    // Add this to your Infrastructure services
    public class VerificationCodeCleanupService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<VerificationCodeCleanupService> _logger;
        private readonly TimeSpan _cleanupInterval = TimeSpan.FromHours(12);

        public VerificationCodeCleanupService(
            IServiceProvider services,
            ILogger<VerificationCodeCleanupService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _services.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var cutoff = DateTime.UtcNow.AddHours(-24);
                    var oldCodes = await dbContext.EmailVerificationCodes
                        .Where(v => v.LastRequestTime < cutoff)
                        .ToListAsync(stoppingToken);

                    if (oldCodes.Any())
                    {
                        dbContext.EmailVerificationCodes.RemoveRange(oldCodes);
                        await dbContext.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation($"Cleaned up {oldCodes.Count} old verification codes");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error cleaning up verification codes");
                }

                await Task.Delay(_cleanupInterval, stoppingToken);
            }
        }
    }
}
