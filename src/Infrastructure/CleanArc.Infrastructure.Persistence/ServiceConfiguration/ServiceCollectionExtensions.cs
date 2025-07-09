using CleanArc.Application.Contracts.Mappers;
using CleanArc.Application.Contracts.Persistence;
using CleanArc.Application.Contracts.Providers;
using CleanArc.Application.Services.Aggregators;
using CleanArc.Domain.Interfaces.Services;
using CleanArc.Infrastructure.Persistence.Configuration.HotelProvidersConfig;
using CleanArc.Infrastructure.Persistence.Providers.BookingWhizz;
using CleanArc.Infrastructure.Persistence.Repositories.Common;
using CleanArc.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace CleanArc.Infrastructure.Persistence.ServiceConfiguration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });
        services.AddHostedService<VerificationCodeCleanupService>();
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


        return services;
    }
}