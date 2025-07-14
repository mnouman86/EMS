using CleanArc.Application.Common.Validation;
using CleanArc.Application.Contracts;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.ResendVerificationEmailCommand;

public record ResendVerificationEmailCommand(
    string Email) : IRequest<OperationResult<bool>>,
    IValidatableModel<ResendVerificationEmailCommand>
{
    public IValidator<ResendVerificationEmailCommand> ValidateApplicationModel(
        ApplicationBaseValidationModelProvider<ResendVerificationEmailCommand> validator)
    {
        var emailDomainValidator = validator.GetService<IEmailDomainValidator>();

        validator.RuleFor(c => c.Email)
            .ValidEmail()
            .NotDisposableEmail(emailDomainValidator); // Uses shared reusable rule


        return validator;
    }
}