using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.VerifyEmailCommand;

public record VerifyEmailCommand
    (string Email, string Code) : IRequest<OperationResult<bool>>,
        IValidatableModel<VerifyEmailCommand>
{
    public IValidator<VerifyEmailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<VerifyEmailCommand> validator)
    {
        validator.RuleFor(c => c.Email)
            .EmailAddress()
            .NotEmpty()
            .WithMessage("Please enter a valid email");

        validator.RuleFor(x => x.Code).NotEmpty().Length(6);

        return validator;
    }
};