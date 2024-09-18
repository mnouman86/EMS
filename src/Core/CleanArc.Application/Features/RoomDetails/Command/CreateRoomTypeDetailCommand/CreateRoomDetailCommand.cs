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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.RoomDetails.Command.CreateRoomDetailCommand;

public record CreateRoomDetailCommand(int? HotelID, int? RoomTypeID, int? RoomSizeUnitID, string? RoomSize, bool? IsBathroomPrivate,
    decimal? Price, decimal? AdditionalMatricCharges, string? RoomNumber, bool? IsAvailable, int? CreatedBy, bool? IsRefundable, bool? IsCancelation) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateRoomDetailCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateRoomDetailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateRoomDetailCommand> validator)
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
