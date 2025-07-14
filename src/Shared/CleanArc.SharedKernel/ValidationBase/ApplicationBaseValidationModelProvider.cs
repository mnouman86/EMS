using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace CleanArc.SharedKernel.ValidationBase;

public class ApplicationBaseValidationModelProvider<TApplicationModel>:AbstractValidator<TApplicationModel>
{
    //public IServiceScope ServiceProvider { get; }

    //public ApplicationBaseValidationModelProvider(IServiceScope serviceProvider)
    //{
    //    ServiceProvider = serviceProvider;
    //}
    public IServiceScope ServiceScope { get; }

    public ApplicationBaseValidationModelProvider(IServiceScope serviceScope)
    {
        ServiceScope = serviceScope;
    }

    /// <summary>
    /// Resolves a scoped service (like IEmailDomainValidator)
    /// </summary>
    public TService GetService<TService>() where TService : notnull
    {
        return ServiceScope.ServiceProvider.GetRequiredService<TService>();
    }

    /// <summary>
    /// Provides standard RuleFor<T> access
    /// </summary>
    public IRuleBuilderInitial<TApplicationModel, TProperty> RuleFor<TProperty>(
        Expression<Func<TApplicationModel, TProperty>> expression)
    {
        return base.RuleFor(expression);
    }

}