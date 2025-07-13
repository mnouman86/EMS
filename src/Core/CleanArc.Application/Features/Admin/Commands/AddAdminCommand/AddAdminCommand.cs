using CleanArc.Application.Common.Validation;
using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.AddAdminCommand;

public record AddAdminCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword,
    int RoleId) : IRequest<OperationResult<bool>>,
    IValidatableModel<AddAdminCommand>
{
    public IValidator<AddAdminCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AddAdminCommand> validator)
    {
        // First Name
        validator.RuleFor(c => c.FirstName)
            .ValidName("First name");

        // Last Name
        validator.RuleFor(c => c.LastName)
            .ValidName("Last name");

        // Email
        validator.RuleFor(c => c.Email)
            .ValidEmail();

        // Password
        validator.RuleFor(c => c.Password)
            .ValidPassword();

        // Confirm Password
        validator.RuleFor(c => c.ConfirmPassword)
            .NotEmpty().WithMessage("Please confirm your password");

        // Password match
        validator.RuleFor(c => c)
            .Must(x => x.Password == x.ConfirmPassword)
            .WithMessage("Passwords do not match")
            .When(x => !string.IsNullOrWhiteSpace(x.Password) && !string.IsNullOrWhiteSpace(x.ConfirmPassword));

        // Role ID
        validator.RuleFor(c => c.RoleId)
            .NotEmpty().WithMessage("Role is required")
            .GreaterThan(0).WithMessage("Please select a valid role")
            .LessThan(100).WithMessage("Role ID must be less than 100");

        return validator;
    }
}