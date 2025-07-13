using CleanArc.Application.Contracts;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Domain.Interfaces.Services;
using CleanArc.Infrastructure.Persistence.Common.Validation;
using CleanArc.Infrastructure.Persistence.Repositories.Common;
using CleanArc.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArc.Infrastructure.Persistence.ServiceConfiguration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration, string contentRootPath)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });
        services.AddHostedService<VerificationCodeCleanupService>();
        var disposableEmailListPath = Path.Combine(contentRootPath, "Infrastructure", "Common", "Validation", "disposable_domains.txt");

        services.AddSingleton<IEmailDomainValidator>(new EmailDomainValidator(disposableEmailListPath));

        return services;
    }
}