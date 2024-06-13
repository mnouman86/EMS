using CleanArc.Application.Features.SearchHotel.Commands.UpdateSearchHotelCommand;
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
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.SearchBusinessCarDetail.Command.UpdateSearchBusinessCarDetail;

public record UpdateSearchBusinessCarDetailCommand(int ID, int HotelID, int CityID, String HotelName, String CityName, String CityDescription, String RoomTypeName, String RoomTypeDescription, Decimal RoomDetailPrice) : IRequest<OperationResult<bool>>,
IValidatableModel<UpdateSearchBusinessCarDetailCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateSearchBusinessCarDetailCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateSearchBusinessCarDetailCommand> validator)
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
