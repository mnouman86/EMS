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

namespace CleanArc.Application.Features.ActivityDisabilityMapping.Commands.UpdateActivityDisabilityMappingCommand;
public record UpdateActivityDisabilityMappingCommand(int Id, String? DisabilityOptionIDs, int? ActivityID ,int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateActivityDisabilityMappingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityDisabilityMappingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityDisabilityMappingCommand> validator)
    {

        validator.RuleFor(c => c. DisabilityOptionIDs)
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

