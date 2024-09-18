using CleanArc.Application.Features.SearchHotelRoomDetail.Command.CreateSearchHotelRoomDetail;
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

namespace CleanArc.Application.Features.SearchRoomAmenities.Commands.CreateSearchRoomAmenities;

public  record CreateSearchRoomAmenitiesCommand(int HotelID, int CityID, String HotelName, String CityName, String CityDescription, String RoomTypeName, String RoomTypeDescription, Decimal RoomDetailPrice) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateSearchRoomAmenitiesCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateSearchRoomAmenitiesCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateSearchRoomAmenitiesCommand> validator)
    {
        validator.RuleFor(c => c.HotelName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.CityDescription)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Description");
        return validator;
    }
}
