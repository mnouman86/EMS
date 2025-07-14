using CleanArc.Application.Common.Validation;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Admin.Commands.ChangePasswordCommand;


public record ChangePasswordCommand(
    string CurrentPassword,
    string NewPassword,
    string ConfirmNewPassword) : IRequest<OperationResult<bool>>,
    IValidatableModel<ChangePasswordCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }

    public IValidator<ChangePasswordCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ChangePasswordCommand> validator)
    {
        // Current password is required
        validator.RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithMessage("Current password is required");

        // New password must be strong
        validator.RuleFor(x => x.NewPassword)
            .ValidPassword(); // Reusable and strong rule

        // Confirm new password
        validator.RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty()
            .WithMessage("Please confirm your new password");

        // Match validation only if both are provided
        validator.RuleFor(x => x)
            .Must(x => x.NewPassword == x.ConfirmNewPassword)
            .WithMessage("New password and confirm password do not match")
            .When(x => !string.IsNullOrWhiteSpace(x.NewPassword) && !string.IsNullOrWhiteSpace(x.ConfirmNewPassword));

        return validator;
    }
}