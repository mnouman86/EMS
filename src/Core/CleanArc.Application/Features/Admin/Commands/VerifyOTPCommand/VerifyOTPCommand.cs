using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Jwt;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CleanArc.Application.Features.Admin.Commands.VerifyOTPCommand
{
    public record VerifyOTPCommand(
        int UserId,
        string Code) : IRequest<OperationResult<AccessToken>>,
        IValidatableModel<VerifyOTPCommand>
    {
        public IValidator<VerifyOTPCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<VerifyOTPCommand> validator)
        {
            // User ID must be valid
            validator.RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("Invalid user ID");

            // Code must be 6-digit numeric
            validator.RuleFor(x => x.Code)
                .NotEmpty().WithMessage("OTP code is required")
                .Length(6).WithMessage("OTP code must be exactly 6 characters")
                .Matches("^[0-9]{6}$").WithMessage("OTP code must be numeric and 6 digits");
            //.Matches("^[a-zA-Z0-9]{6}$").WithMessage("OTP code must be 6 alphanumeric characters");

            return validator;
        }
    }
}
