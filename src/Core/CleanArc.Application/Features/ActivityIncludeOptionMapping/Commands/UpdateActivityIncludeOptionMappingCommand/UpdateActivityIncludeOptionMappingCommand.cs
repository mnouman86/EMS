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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.ActivityIncludeOptionMapping.Commands.UpdateActivityIncludeOptionMappingCommand;
public record UpdateActivityIncludeOptionMappingCommand(int ID, string? IncludeOptionsLookUpID, int? ActivityID, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateActivityIncludeOptionMappingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityIncludeOptionMappingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityIncludeOptionMappingCommand> validator)
    {
        validator.RuleFor(c => c.IncludeOptionsLookUpID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid IncludeOptionsLookUpID");
        validator.RuleFor(c => c.ActivityID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ActivityID");
        return validator;
    }
}

