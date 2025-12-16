using CleanArc.Application.Contracts;
using CleanArc.Application.Contracts.Mappers;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Contracts.Providers;
using CleanArc.Application.Services.Aggregators;
using CleanArc.Domain.Interfaces.Services;
using CleanArc.Infrastructure.Persistence.Common.Validation;
using CleanArc.Infrastructure.Persistence.Configuration.FlightsConfig;
using CleanArc.Infrastructure.Persistence.Configuration.HotelProvidersConfig;
using CleanArc.Infrastructure.Persistence.Providers.BookingWhizz;
using CleanArc.Infrastructure.Persistence.Providers.KPlus;
using CleanArc.Infrastructure.Persistence.Providers.Mosafir;
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

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });
        services.AddHostedService<VerificationCodeCleanupService>();

        // Add KPlus options from configuration (add this near MosafirOptions registration)
        services.Configure<KPlusOptions>(configuration.GetSection("KPlus"));

        services.Configure<MosafirOptions>(configuration.GetSection("Mosafir"));

        // Register BookingWhizz 3rd-party hotel integration services
        // Register BookingWhizzSettings config section
        services.Configure<BookingWhizzSettings>(
            configuration.GetSection("HotelProviders:BookingWhizz"));

        // Register the response mapper
        services.AddScoped<IHotelResponseMapper<XDocument>, BookingWhizzResponseMapper>();

        // Register the HttpClient for BookingWhizzProvider
        services.AddHttpClient<BookingWhizzProvider>();

        // Register BookingWhizzProvider as one of the IHotelProvider implementations
        services.AddScoped<IHotelProvider, BookingWhizzProvider>();

        // Register the aggregator (optional if using multiple providers)
        services.AddScoped<HotelProviderAggregator>();

        //services.AddHttpClient<BookingWhizzProvider>(client =>
        //{
        //    client.BaseAddress = new Uri("http://beapi.bookingwhizz.com/");
        //    client.DefaultRequestHeaders.Add("Accept", "application/xml");
        //});


        // Register typed HttpClient for Mosafir provider
        services.AddHttpClient<MosafirFlightProvider>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<MosafirOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
            // add default headers if needed:
            if (!string.IsNullOrWhiteSpace(opts.ApiKey))
                client.DefaultRequestHeaders.Add("X-Api-Key", opts.ApiKey);
        });

        // Register the typed HttpClient for KPlus provider
        services.AddHttpClient<KPlusFlightProvider>((sp, client) =>
        {
            var opts = sp.GetRequiredService<IOptions<KPlusOptions>>().Value;
            client.BaseAddress = new Uri(opts.BaseUrl);
            client.Timeout = TimeSpan.FromSeconds(opts.TimeoutSeconds);
        });

        // Resolve interface to typed provider
        services.AddScoped<IFlightProvider>(sp => sp.GetRequiredService<MosafirFlightProvider>());

        // Register KPlus provider
        services.AddScoped<IKPlusFlightProvider, KPlusFlightProvider>();

        // <<< Register lookup implementation here >>>
        services.AddSingleton<ILookupService, JsonLookupService>();

        var disposableEmailListPath = Path.Combine(contentRootPath, "Infrastructure", "Common", "Validation", "disposable_domains.txt");

        services.AddSingleton<IEmailDomainValidator>(new EmailDomainValidator(disposableEmailListPath));

        return services;
    }
}