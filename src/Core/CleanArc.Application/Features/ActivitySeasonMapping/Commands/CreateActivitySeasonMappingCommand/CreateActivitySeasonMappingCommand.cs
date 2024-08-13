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

namespace CleanArc.Application.Features.ActivitySeasonMapping.Commands.CreateActivitySeasonMappingCommand;
public record CreateActivitySeasonMappingCommand(String? SeasonIDs, int? ActivityID, int? CreatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateActivitySeasonMappingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivitySeasonMappingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivitySeasonMappingCommand> validator)
    {
        validator.RuleFor(c => c.SeasonIDs)
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

