using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Policy.Command.UpdatePolicyCommand;

public record UpdatePolicyCommand(int Id, int? RatePlanTypeID, int? RefundPolicyTypeLookUpID, decimal? DeductionPercentage, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdatePolicyCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdatePolicyCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdatePolicyCommand> validator)
    {
        validator.RuleFor(c => c.DeductionPercentage)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid percentage");
        return validator;
    }
}
