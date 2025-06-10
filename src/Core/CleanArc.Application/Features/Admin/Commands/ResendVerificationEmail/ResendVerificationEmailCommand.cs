using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.ResendVerificationEmailCommand;

public record ResendVerificationEmailCommand
    (string Email) : IRequest<OperationResult<bool>>,
        IValidatableModel<ResendVerificationEmailCommand>
{
    public IValidator<ResendVerificationEmailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ResendVerificationEmailCommand> validator)
    {
        validator.RuleFor(c => c.Email)
            .EmailAddress()
            .NotEmpty()
            .WithMessage("Please enter a valid email");

        

        return validator;
    }
};