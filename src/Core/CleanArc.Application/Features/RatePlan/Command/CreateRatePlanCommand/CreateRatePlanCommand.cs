using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
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

namespace CleanArc.Application.Features.RatePlan.Command.CreateRatePlanCommand
{
    public record CreateRatePlanCommand(RatePlanRequestDto RatePlanRequest) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateRatePlanCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<CreateRatePlanCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateRatePlanCommand> validator)
        {
            validator.RuleFor(c => c.RatePlanRequest)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a valid request");
            return validator;
        }
    }


}
