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

namespace CleanArc.Application.Features.RefundPolicy.Command.UpdateRefundPolicyCommand;

public record UpdateRefundPolicyCommand(int Id, int? GenericTitleId, int? ServiceTypeEnumId, int? RefundPolicyTypeLookUpID, decimal? DeductionPercentage, int? CultureId, string Description) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdateRefundPolicyCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateRefundPolicyCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateRefundPolicyCommand> validator)
    {
        validator.RuleFor(c => c.DeductionPercentage)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid percentage");
        return validator;
    }
}
