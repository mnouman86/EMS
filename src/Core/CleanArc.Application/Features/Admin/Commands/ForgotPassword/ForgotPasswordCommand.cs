using CleanArc.Application.Common.Validation;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.ForgotPasswordCommand;

public record ForgotPasswordCommand(
    string Email) : IRequest<OperationResult<bool>>,
    IValidatableModel<ForgotPasswordCommand>
{
    public IValidator<ForgotPasswordCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ForgotPasswordCommand> validator)
    {
        validator.RuleFor(c => c.Email)
            .ValidEmail(); // Uses centralized shared validation rule

        return validator;
    }
}