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

namespace CleanArc.Application.Features.RoomDetails.Command.CreateRoomDetailCommand;

public record 
    CreateRoomDetailCommand(int? GenericTitleId, int? RoomTypeLookUpId, int? RoomSizeUnitLookUpId, 
    string? RoomSize, bool? IsSharedBathroom,string? Description,
    decimal? Price, decimal? AdditionalMattressCharges, string? NoOfRooms, bool? IsAvailable, 
    int? CultureId, bool? IsPartiallyRefundable, bool? IsFullyRefundable, int[]? RoomViewLookUpId, int[]? OutDoorLookUpId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateRoomDetailCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateRoomDetailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateRoomDetailCommand> validator)
    {
        validator.RuleFor(c => c.GenericTitleId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid HotelID");
        validator.RuleFor(c => c.RoomTypeLookUpId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a RoomTypeID");
        validator.RuleFor(c => c.RoomSizeUnitLookUpId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a RoomSizeUnitID");
       
        return validator;
    }
}
