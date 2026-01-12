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

namespace CleanArc.Application.Features.NoShowPolicy.Command.UpdateNoShowPolicyCommand;

public record UpdateNoShowPolicyCommand(int Id, int? RatePlanTypeID, bool? OneNight, decimal? DeductionPercentage, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdateNoShowPolicyCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateNoShowPolicyCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateNoShowPolicyCommand> validator)
    {
        validator.RuleFor(c => c.DeductionPercentage)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid percentage");
        return validator;
    }
}
