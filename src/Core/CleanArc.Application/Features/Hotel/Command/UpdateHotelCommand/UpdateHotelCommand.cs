using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
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
using CleanArc.Application.Models.Request;
using System.Text.Json.Serialization; 
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Language;

namespace CleanArc.Application.Features.Hotel.Command.UpdateHotelCommand;

public record UpdateHotelCommand(int Id, string? Name, int? BusinessId, int? ThirdPartyStayId,int? Stars,
    string? PostalCode,
    string? AddressLine1,
    string? AddressLine2,
    int? CountryLookUpId, int? StateLookUpId, int? CityLookUpId, int[]? LanguageLookUpId,
    string? Latitude, string? Longitude,
    string? MobileNumber,
    string? PhoneNumber, string? Email,
    string? FocalPersonName, bool? IsChanelManager, bool? IsRating, bool? IsChain,
    int? ServiceId, int? ServiceCategoryId, string? CheckInFrom, string? CheckInTo,
    string? CheckOutFrom, string? CheckOutTo, string? About, int? CultureId,
    string? RefundPolicy, string? NonRefundPolicy, string? CancellationPolicy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateHotelCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateHotelCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateHotelCommand> validator)
    {
        validator.RuleFor(c => c.Name)
             .NotEmpty()
             .NotNull()
             .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.CheckInFrom)
            .NotEmpty()
    .NotNull()
    .Matches(@"^(0?[1-9]|1[0-2]):([0-5]?[0-9]) (AM|PM)$")
    .WithMessage("CheckInFrom must be in a valid 12-hour format (e.g., 1:05 AM, 12:00 PM, 01:5 PM)");
        validator.RuleFor(c => c.CheckInTo)
            .NotEmpty()
    .NotNull()
    .Matches(@"^(0?[1-9]|1[0-2]):([0-5]?[0-9]) (AM|PM)$")
    .WithMessage("CheckInFrom must be in a valid 12-hour format (e.g., 1:05 AM, 12:00 PM, 01:5 PM)");
        validator.RuleFor(c => c.CheckOutFrom)
            .NotEmpty()
    .NotNull()
    .Matches(@"^(0?[1-9]|1[0-2]):([0-5]?[0-9]) (AM|PM)$")
    .WithMessage("CheckInFrom must be in a valid 12-hour format (e.g., 1:05 AM, 12:00 PM, 01:5 PM)");
        validator.RuleFor(c => c.CheckOutTo)
            .NotEmpty()
    .NotNull()
    .Matches(@"^(0?[1-9]|1[0-2]):([0-5]?[0-9]) (AM|PM)$")
    .WithMessage("CheckInFrom must be in a valid 12-hour format (e.g., 1:05 AM, 12:00 PM, 01:5 PM)");

        return validator;
    }
}
