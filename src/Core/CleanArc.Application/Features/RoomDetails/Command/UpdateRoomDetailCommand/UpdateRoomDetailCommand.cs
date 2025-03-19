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

namespace CleanArc.Application.Features.RoomDetails.Command.UpdateRoomDetailCommand;

public record UpdateRoomDetailCommand(int ID, int? HotelID, int? RoomTypeID, int? RoomSizeUnitID, string? RoomSize, bool? IsBathroomPrivate,
    decimal? Price, decimal? AdditionalMatricCharges, string? RoomNumber, bool? IsAvailable, int? UpdatedBy, bool? IsRefundable, bool? IsCancelation) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateRoomDetailCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateRoomDetailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateRoomDetailCommand> validator)
    {
        validator.RuleFor(c => c.HotelID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid HotelID");
        validator.RuleFor(c => c.RoomTypeID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a RoomTypeID");
        validator.RuleFor(c => c.RoomSizeUnitID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a RoomSizeUnitID");
        validator.RuleFor(c => c.RoomSize)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a RoomSize");
        validator.RuleFor(c => c.IsBathroomPrivate)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a IsBathroomPrivate");
        validator.RuleFor(c => c.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Price");
        validator.RuleFor(c => c.AdditionalMatricCharges)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a AdditionalMatricCharges");
        validator.RuleFor(c => c.RoomNumber)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a RoomNumber");
        ////validator.RuleFor(c => c.IsAvailable)
        ////    .NotEmpty()
        ////    .NotNull()
        //.WithMessage("Please enter a Description");
        return validator;
    }
}
