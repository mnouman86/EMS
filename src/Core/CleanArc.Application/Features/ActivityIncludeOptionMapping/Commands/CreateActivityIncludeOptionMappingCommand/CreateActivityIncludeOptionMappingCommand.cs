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

namespace CleanArc.Application.Features.ActivityIncludeOptionMapping.Commands.CreateActivityIncludeOptionMappingCommand;
public record CreateActivityIncludeOptionMappingCommand(string? IncludeOptionIDs, int? ActivityID, int? CreatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateActivityIncludeOptionMappingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityIncludeOptionMappingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityIncludeOptionMappingCommand> validator)
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

