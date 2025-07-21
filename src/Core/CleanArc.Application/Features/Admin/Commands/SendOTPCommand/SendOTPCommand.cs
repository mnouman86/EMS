using CleanArc.Application.Common.Validation;
using CleanArc.Application.Models.Common;
using CleanArc.Domain.Enums;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Admin.Commands.SendOTPCommand
{
    public record SendOTPCommand(
    string UserName,
    string Password) : IRequest<OperationResult<bool>>,
    IValidatableModel<SendOTPCommand>
    {
        public IValidator<SendOTPCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<SendOTPCommand> validator)
        {
            // If UserName is email-based
            validator.RuleFor(x => x.UserName)
                .ValidEmail();

            // Strong password
            validator.RuleFor(x => x.Password)
                .ValidPassword();

            // Enum validation
            //validator.RuleFor(x => x.Method)
            //    .IsInEnum()
            //    .WithMessage("Invalid OTP delivery method selected");

            return validator;
        }
    }
}
