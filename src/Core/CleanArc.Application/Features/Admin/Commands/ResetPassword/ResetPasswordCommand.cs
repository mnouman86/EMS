using CleanArc.Application.Common.Validation;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.ResetPasswordCommand;

public record ResetPasswordCommand(
    string Email,
    string Token,
    string NewPassword,
    string ConfirmNewPassword) : IRequest<OperationResult<bool>>,
    IValidatableModel<ResetPasswordCommand>
{
    public IValidator<ResetPasswordCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ResetPasswordCommand> validator)
    {
        // Validate Email
        validator.RuleFor(x => x.Email)
            .ValidEmail();

        // Token validation
        validator.RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Reset token is required");

        // New password validation
        validator.RuleFor(x => x.NewPassword)
            .ValidPassword();

        // Confirm password
        validator.RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty().WithMessage("Please confirm your new password");

        validator.RuleFor(x => x)
            .Must(x => x.NewPassword == x.ConfirmNewPassword)
            .WithMessage("New password and confirm password do not match")
            .When(x => !string.IsNullOrWhiteSpace(x.NewPassword) && !string.IsNullOrWhiteSpace(x.ConfirmNewPassword));

        return validator;
    }
}