using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.RatePlan;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization; 
using System.Threading.Tasks;

namespace CleanArc.Application.Features.RatePlan.Command.UpdateRatePlanCommand;

public record UpdateRatePlanCommand(RatePlanRequestDto RatePlanRequest) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdateRatePlanCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateRatePlanCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateRatePlanCommand> validator)
    {
        validator.RuleFor(c => c.RatePlanRequest)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid request");
        return validator;
    }
}

