using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.AddAdminCommand;

public record AddAdminCommand
    (string FirstName, string LastName, string Email, string Password, string ConfirmPassword, int RoleId) : IRequest<OperationResult<bool>>,
        IValidatableModel<AddAdminCommand>
{
    public IValidator<AddAdminCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AddAdminCommand> validator)
    {
        validator.RuleFor(c => c.Email)
            .EmailAddress()
            .WithMessage("Please enter a valid email");

        validator.RuleFor(c => c.FirstName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please specify a valid username");

        validator.RuleFor(c => c.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long");

        validator.RuleFor(c => c.ConfirmPassword)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please confirm your password");

        validator.RuleFor(c => c)
            .Must(x => x.Password == x.ConfirmPassword)
            .WithMessage("Passwords do not match")
            .When(x => !string.IsNullOrEmpty(x.Password) && !string.IsNullOrEmpty(x.ConfirmPassword));

        validator
            .RuleFor(c => c.RoleId)
            .GreaterThan(0)
            .WithMessage("Please select a valid role");

        return validator;
    }
};