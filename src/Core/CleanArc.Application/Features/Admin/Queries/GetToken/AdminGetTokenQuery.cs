using CleanArc.Application.Common.Validation;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Jwt;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;

namespace CleanArc.Application.Features.Admin.Queries.GetToken;

public record AdminGetTokenQuery(
    string UserName,
    string Password) : IRequest<OperationResult<AccessToken>>,
    IValidatableModel<AdminGetTokenQuery>
{
    public IValidator<AdminGetTokenQuery> ValidateApplicationModel(ApplicationBaseValidationModelProvider<AdminGetTokenQuery> validator)
    {
        // Username validation (alphanumeric with underscores/dots)
        validator.RuleFor(c => c.UserName)
            .ValidEmail();
            //.NotEmpty().WithMessage("Username is required")
            //.Length(3, 50).WithMessage("Username must be between 3 and 50 characters")
            //.Matches(@"^[a-zA-Z0-9_.]+$").WithMessage("Username can only contain letters, numbers, underscores, or dots");

        // Strong password validation
        validator.RuleFor(c => c.Password)
            .ValidPassword();

        return validator;
    }
}