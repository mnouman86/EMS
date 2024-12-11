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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.CarRentalSearchFilter.Command.UpdateCarRentalSearchFilter;

public record UpdateCarRentalSearchFilterCommand(int ID, int HotelID, int CityID, String Name, String CityName, String CityDescription, String RoomTypeName, String RoomTypeDescription, Decimal RoomDetailPrice) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<UpdateCarRentalSearchFilterCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateCarRentalSearchFilterCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateCarRentalSearchFilterCommand> validator)
    {
        validator.RuleFor(c => c.Name)
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
