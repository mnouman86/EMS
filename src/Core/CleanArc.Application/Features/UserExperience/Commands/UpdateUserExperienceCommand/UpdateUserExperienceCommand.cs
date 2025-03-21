using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.UserExperience.Commands.UpdateUserExperienceCommand;
public record UpdateUserExperienceCommand(int Id, int? UserID, string? UserIntrestIDs, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateUserExperienceCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateUserExperienceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateUserExperienceCommand> validator)
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

