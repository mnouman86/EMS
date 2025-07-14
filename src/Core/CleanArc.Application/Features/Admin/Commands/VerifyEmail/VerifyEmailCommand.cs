using CleanArc.Application.Common.Validation;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.VerifyEmailCommand;

public record VerifyEmailCommand(
    string Email,
    string Code) : IRequest<OperationResult<bool>>,
    IValidatableModel<VerifyEmailCommand>
{
    public IValidator<VerifyEmailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<VerifyEmailCommand> validator)
    {
        // Email validation
        validator.RuleFor(c => c.Email)
            .ValidEmail();

        // Code validation
        validator.RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Verification code is required")
            .Length(6).WithMessage("Verification code must be exactly 6 characters")
            .Matches("^[0-9]{6}$").WithMessage("Verification code must be 6 digits"); // Optional: use alphanumeric if needed
        //.Matches("^[a-zA-Z0-9]{6}$").WithMessage("Verification code must be 6 alphanumeric characters");

        return validator;
    }
}