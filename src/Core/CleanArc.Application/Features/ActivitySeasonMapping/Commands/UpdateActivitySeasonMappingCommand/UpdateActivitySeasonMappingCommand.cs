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
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.ActivitySeasonMapping.Commands.UpdateActivitySeasonMappingCommand;
public record UpdateActivitySeasonMappingCommand(int ID, int? SeasonLookUpID, int? ActivityID, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateActivitySeasonMappingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivitySeasonMappingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivitySeasonMappingCommand> validator)
    {
        validator.RuleFor(c => c.SeasonLookUpID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid SeasonLookUpID");
        validator.RuleFor(c => c.ActivityID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ActivityID");
        return validator;
    }
}

