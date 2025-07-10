using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Admin.Commands.ResetPasswordCommand;

public record ResetPasswordCommand
    (string Email, string Token, string NewPassword, string ConfirmNewPassword) : IRequest<OperationResult<bool>>,
        IValidatableModel<ResetPasswordCommand>
{
    public IValidator<ResetPasswordCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ResetPasswordCommand> validator)
    {
       validator.RuleFor(x => x.Email).NotEmpty().EmailAddress();
        validator.RuleFor(x => x.Token).NotEmpty();
        validator.RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6);
        validator.RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword)
            .WithMessage("Passwords do not match");

        return validator;
    }
};