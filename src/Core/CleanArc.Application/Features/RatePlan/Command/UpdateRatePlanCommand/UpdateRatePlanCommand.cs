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

namespace CleanArc.Application.Features.RatePlan.Command.UpdateRatePlanCommand;

public record UpdateRatePlanCommand(int Id, int? RoomTypeId, int? GuestQuantity, decimal? DefaultRate, string? Description, string RatePlanName, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdateRatePlanCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateRatePlanCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateRatePlanCommand> validator)
    {
        validator.RuleFor(c => c.RatePlanName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        return validator;
    }
}

