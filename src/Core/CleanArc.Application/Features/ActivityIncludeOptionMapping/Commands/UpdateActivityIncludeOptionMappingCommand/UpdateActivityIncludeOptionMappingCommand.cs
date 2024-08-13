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

namespace CleanArc.Application.Features.ActivityIncludeOptionMapping.Commands.UpdateActivityIncludeOptionMappingCommand;
public record UpdateActivityIncludeOptionMappingCommand(int ID, string? IncludeOptionIDs, int? ActivityID, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateActivityIncludeOptionMappingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityIncludeOptionMappingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityIncludeOptionMappingCommand> validator)
    {
        validator.RuleFor(c => c.IncludeOptionIDs)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid IncludeOptionIDs");
        validator.RuleFor(c => c.ActivityID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ActivityID");
        return validator;
    }
}

