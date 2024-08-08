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

namespace CleanArc.Application.Features.ActivityImageMapping.Commands.UpdateActivityImageMappingCommand;
public record UpdateActivityImageMappingCommand(int ID, int? ActivityID, string? ImagePath, string? ImageTitle, bool? IsMain, int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateActivityImageMappingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityImageMappingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityImageMappingCommand> validator)
    {
        validator.RuleFor(c => c.ActivityID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid ActivityID");
        validator.RuleFor(c => c.ImagePath)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ImagePath");
        validator.RuleFor(c => c.ImageTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ImageTitle");
        return validator;
    }
}

