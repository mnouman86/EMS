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

namespace CleanArc.Application.Features.LastMinuteDeal.Command.UpdateLastMinuteDealCommand;

public record UpdateLastMinuteDealCommand(int Id, int ServiceTypeEnumId,int GenericTitleId,
    int FilterCategoryLookUpId, decimal? Discount, DateTime? StartDate, DateTime? EndDate, int? Priority, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdateLastMinuteDealCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateLastMinuteDealCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateLastMinuteDealCommand> validator)
    {
        validator.RuleFor(c => c.ServiceTypeEnumId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Enum Service Type Id");
        return validator;
    }
}
