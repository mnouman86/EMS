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

namespace CleanArc.Application.Features.ActivityPerGroupPrice.Commands.CreateActivityPerGroupPriceCommand;
public record CreateActivityPerGroupPriceCommand(int? ActivityID, int? MinGroupSize, int? MaxGroupSize, decimal? Price, int? CreatedBy,int? CultureId) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateActivityPerGroupPriceCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityPerGroupPriceCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityPerGroupPriceCommand> validator)
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

