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

namespace CleanArc.Application.Features.ActivityPerGroupPrice.Commands.UpdateActivityPerGroupPriceCommand;
public record UpdateActivityPerGroupPriceCommand(int ID, int? ActivityID, int? MinGroupSize, int? MaxGroupSize, decimal? Price, int? UpdatedBy, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateActivityPerGroupPriceCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityPerGroupPriceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityPerGroupPriceCommand> validator)
    {
        validator.RuleFor(c => c.ActivityID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid ActivityID");
        validator.RuleFor(c => c.MinGroupSize)
     .NotEmpty()
     .NotNull()
     .WithMessage("Please enter a MinGroupSize");
        validator.RuleFor(c => c.MaxGroupSize)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a MaxGroupSize");
        validator.RuleFor(c => c.Price)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a Size");
        return validator;
    }
}

