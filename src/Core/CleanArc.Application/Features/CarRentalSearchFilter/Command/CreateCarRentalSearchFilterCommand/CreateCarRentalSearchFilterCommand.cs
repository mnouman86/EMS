using CleanArc.Application.Features.SearchHotel.Commands.CreateSearchHotelCommand;
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

namespace CleanArc.Application.Features.CarRentalSearchFilter.Command.CreateCarRentalSearchFilter;

public record CreateCarRentalSearchFilterCommand(int HotelID, int CityID, String HotelName, String CityName, String CityDescription, String RoomTypeName, String RoomTypeDescription, Decimal RoomDetailPrice) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateCarRentalSearchFilterCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateCarRentalSearchFilterCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateCarRentalSearchFilterCommand> validator)
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
