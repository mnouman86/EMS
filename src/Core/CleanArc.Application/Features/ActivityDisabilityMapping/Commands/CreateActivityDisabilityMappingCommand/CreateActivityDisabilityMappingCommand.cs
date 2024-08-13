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

namespace CleanArc.Application.Features.ActivityDisabilityMapping.Commands.CreateActivityDisabilityMappingCommand;
public record CreateActivityDisabilityMappingCommand(String? DisabilityOptionIDs, int? ActivityID, int? CreatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateActivityDisabilityMappingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityDisabilityMappingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityDisabilityMappingCommand> validator)
    {
      
        validator.RuleFor(c => c.DisabilityOptionIDs)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter a DisabilityOptionIDs");
        validator.RuleFor(c => c.ActivityID)
    .NotEmpty()
    .NotNull()
    .WithMessage("Please enter a ActivityID");
        return validator;
    }
}

