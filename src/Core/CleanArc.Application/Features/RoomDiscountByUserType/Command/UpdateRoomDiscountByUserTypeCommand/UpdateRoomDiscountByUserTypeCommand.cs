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

namespace CleanArc.Application.Features.RoomDiscountByUserType.Command.UpdateRoomDiscountByUserTypeCommand;

public record UpdateRoomDiscountByUserTypeCommand(int Id, int? GenericTitleId, string? Description, int? RoomDetailId, string? RatePlanTypeIds, string? UserTypeIds, int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateRoomDiscountByUserTypeCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateRoomDiscountByUserTypeCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateRoomDiscountByUserTypeCommand> validator)
    {
        validator.RuleFor(c => c.GenericTitleId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Hotel");
        return validator;
    }
}
