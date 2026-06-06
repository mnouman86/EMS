using CleanArc.Application.Contracts;
using CleanArc.Application.Contracts.Notifications;
using CleanArc.Application.Contracts.Pdf;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Models.Expense;
using CleanArc.Application.Models.Fee;
using CleanArc.Application.Models.Finance;
using CleanArc.Domain.Interfaces.Services;
using CleanArc.Infrastructure.Persistence.Common.Validation;
using CleanArc.Infrastructure.Persistence.Pdf;
using CleanArc.Infrastructure.Persistence.Repositories.Common;
using CleanArc.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace CleanArc.Infrastructure.Persistence.ServiceConfiguration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration, string contentRootPath)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Access-control: cached per-user permission lookups for authorization.
        services.AddMemoryCache();
        services.AddScoped<CleanArc.Application.Contracts.Identity.IUserPermissionProvider,
                           CleanArc.Infrastructure.Persistence.Services.UserPermissionProvider>();

        // Per-request teacher data scope (class teacher + TeacherAssignment classes).
        services.AddScoped<CleanArc.Application.Contracts.Identity.ITeacherScopeContext,
                           CleanArc.Infrastructure.Persistence.Services.TeacherScopeContext>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });
        services.AddHostedService<VerificationCodeCleanupService>();

        
        var disposableEmailListPath = Path.Combine(contentRootPath, "Infrastructure", "Common", "Validation", "disposable_domains.txt");

        services.AddSingleton<IEmailDomainValidator>(new EmailDomainValidator(disposableEmailListPath));

        // PDF rendering (QuestPDF) — one-time license configuration
        PdfBootstrapper.Configure();
        services.AddScoped<IPdfRenderer<FeeReceiptModel>, FeeReceiptPdfRenderer>();
        services.AddScoped<IPdfRenderer<SalarySlipModel>, SalarySlipPdfRenderer>();
        services.AddScoped<IPdfRenderer<PnLStatementModel>, PnLStatementPdfRenderer>();

        // Default notification sender: no-op (logs to NotificationLog table only).
        // Replace registration with a Twilio/SMTP/WhatsApp implementation later.
        services.AddScoped<INotificationSender, NoOpNotificationSender>();

        return services;
    }
}