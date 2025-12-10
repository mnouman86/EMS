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

namespace CleanArc.Application.Features.RoomDiscountByUserType.Command.CreateRoomDiscountByUserTypeCommand;

public record CreateRoomDiscountByUserTypeCommand(int? GenericTitleId,  string? Description, int? RoomDetailId, string? RatePlanTypeIds, string? UserTypeIds, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateRoomDiscountByUserTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateRoomDiscountByUserTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateRoomDiscountByUserTypeCommand> validator)
    {
        validator.RuleFor(c => c.GenericTitleId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Hotel");
        return validator;
    }
}
