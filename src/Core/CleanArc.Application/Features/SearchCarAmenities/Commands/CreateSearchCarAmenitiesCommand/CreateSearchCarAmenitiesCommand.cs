using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.SearchCarAmenities.Commands.CreateSearchCarAmenitiesCommand;

public record CreateSearchCarAmenitiesCommand(int HotelID, int CityID, String HotelName, String CityName, String CityDescription, String RoomTypeName, String RoomTypeDescription, Decimal RoomDetailPrice) : IRequest<OperationResult<ResponseEntity>>,
IValidatableModel<CreateSearchCarAmenitiesCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateSearchCarAmenitiesCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateSearchCarAmenitiesCommand> validator)
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
