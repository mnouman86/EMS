using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.Admin.Commands.ChangePasswordCommand;

public record ChangePasswordCommand
    (string CurrentPassword, string NewPassword, string ConfirmNewPassword) : IRequest<OperationResult<bool>>,
        IValidatableModel<ChangePasswordCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<ChangePasswordCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<ChangePasswordCommand> validator)
    {
        validator.RuleFor(x => x.CurrentPassword).NotEmpty();
        validator.RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6);
        validator.RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword)
            .WithMessage("Passwords do not match");

        return validator;
    }
};