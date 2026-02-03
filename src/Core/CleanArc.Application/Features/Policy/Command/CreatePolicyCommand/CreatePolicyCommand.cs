using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
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

namespace CleanArc.Application.Features.Policy.Command.CreatePolicyCommand
{
    public record CreatePolicyCommand(int? GenericTitleId, int? ServiceTypeEnumId,int? RatePlanTypeID, int? RefundPolicyTypeLookUpID, decimal? DeductionPercentage, int? CultureId, string Description) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreatePolicyCommand>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public IValidator<CreatePolicyCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreatePolicyCommand> validator)
        {
            validator.RuleFor(c => c.DeductionPercentage)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter a valid percentage");
            return validator;
        }
    }

    
    }
