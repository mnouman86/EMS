using CleanArc.Application.Common;
using CleanArc.Domain.Interfaces.Services;
using CleanArc.SharedKernel.Extensions;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArc.Application.ServiceConfiguration;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton(GetConfiguredMappingConfig());
        
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
            options.Namespace = "CleanArc.Application.Mediator";
        });
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidateCommandBehavior<,>));
        //services.AddMediator(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());


        return services;
    }
    /// <summary>
    /// Mapster(Mapper) global configuration settings
    /// To learn more about Mapster,
    /// see https://github.com/MapsterMapper/Mapster
    /// </summary>
    /// <returns></returns>
    private static TypeAdapterConfig GetConfiguredMappingConfig()
    {
        var config = TypeAdapterConfig.GlobalSettings;

        IList<IRegister> registers = config.Scan(Assembly.GetExecutingAssembly());

        config.Apply(registers);

        return config;
    }


}