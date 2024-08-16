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

namespace CleanArc.Application.Features.ActivityIDImageMapping.Commands.CreateActivityIDImageMappingCommand;
public record CreateActivityIDImageMappingCommand(int? ActivityID, string? ImagePath, string? ImageTitle, bool? IsMain, int? CreatedBy,int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateActivityIDImageMappingCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityIDImageMappingCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityIDImageMappingCommand> validator)
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

