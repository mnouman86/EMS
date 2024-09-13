using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using System.Text.Json.Serialization;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.UserExperience.Commands.CreateUserExperienceCommand;
public record CreateUserExperienceCommand(int? UserID, string? UserIntrestIDs,  int? CreatedBy, int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateUserExperienceCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateUserExperienceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateUserExperienceCommand> validator)
    {
       
        validator.RuleFor(c => c.UserID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid UserID");
        validator.RuleFor(c => c.UserIntrestIDs)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid UserIntrestIDs");

        return validator;
    }
}

