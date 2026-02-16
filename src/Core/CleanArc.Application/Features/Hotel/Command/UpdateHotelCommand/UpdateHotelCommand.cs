using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.Language;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization; 
using System.Threading.Tasks;

namespace CleanArc.Application.Features.Hotel.Command.UpdateHotelCommand;

public record UpdateHotelCommand(int Id, string? Name, int? BusinessId,int? Stars,
    bool? Status,
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
        //    validator.RuleFor(c => c.CheckInFrom)
        //        .NotEmpty()
        //.NotNull()
        //.Matches(@"^(0?[1-9]|1[0-2]):([0-5]?[0-9]) (AM|PM)$")
        //.WithMessage("CheckInFrom must be in a valid 12-hour format (e.g., 1:05 AM, 12:00 PM, 01:5 PM)");
        //    validator.RuleFor(c => c.CheckInTo)
        //        .NotEmpty()
        //.NotNull()
        //.Matches(@"^(0?[1-9]|1[0-2]):([0-5]?[0-9]) (AM|PM)$")
        //.WithMessage("CheckInFrom must be in a valid 12-hour format (e.g., 1:05 AM, 12:00 PM, 01:5 PM)");
        //    validator.RuleFor(c => c.CheckOutFrom)
        //        .NotEmpty()
        //.NotNull()
        //.Matches(@"^(0?[1-9]|1[0-2]):([0-5]?[0-9]) (AM|PM)$")
        //.WithMessage("CheckInFrom must be in a valid 12-hour format (e.g., 1:05 AM, 12:00 PM, 01:5 PM)");
        //    validator.RuleFor(c => c.CheckOutTo)
        //        .NotEmpty()
        //.NotNull()
        //.Matches(@"^(0?[1-9]|1[0-2]):([0-5]?[0-9]) (AM|PM)$")
        //.WithMessage("CheckInFrom must be in a valid 12-hour format (e.g., 1:05 AM, 12:00 PM, 01:5 PM)");
        // Time format regex: "01:05 PM", "1:05 AM", "12:00 PM"
        const string timeRegex = @"^(0?[1-9]|1[0-2]):([0-5][0-9]) (AM|PM)$";
        const string timeFormat = "h:mm tt";

        validator.RuleFor(c => c.CheckInFrom)
            .NotEmpty().WithMessage("Check-in from time is required")
            .Matches(timeRegex).WithMessage("Check-in from time must be in 12-hour format (e.g., 3:00 PM)");

        //validator.RuleFor(c => c.CheckInTo)
        //    .NotEmpty().WithMessage("Check-in to time is required")
        //    .Matches(timeRegex).WithMessage("Check-in to time must be in 12-hour format (e.g., 4:00 PM)");

        validator.RuleFor(c => c.CheckOutFrom)
            .NotEmpty().WithMessage("Check-out from time is required")
            .Matches(timeRegex).WithMessage("Check-out from time must be in 12-hour format (e.g., 10:00 AM)");

        //validator.RuleFor(c => c.CheckOutTo)
        //    .NotEmpty().WithMessage("Check-out to time is required")
        //    .Matches(timeRegex).WithMessage("Check-out to time must be in 12-hour format (e.g., 12:00 PM)");

        // Time range logic
        //validator.RuleFor(c => c)
        //    .Must(c => ParseTime(c.CheckInFrom) < ParseTime(c.CheckInTo))
        //    .When(c => IsValidTime(c.CheckInFrom) && IsValidTime(c.CheckInTo))
        //    .WithMessage("Check-in 'From' time must be earlier than 'To' time");

        //validator.RuleFor(c => c)
        //    .Must(c => ParseTime(c.CheckOutFrom) < ParseTime(c.CheckOutTo))
        //    .When(c => IsValidTime(c.CheckOutFrom) && IsValidTime(c.CheckOutTo))
        //    .WithMessage("Check-out 'From' time must be earlier than 'To' time");

        // 🚨 Ensure checkout ends before check-in starts (business logic)
        validator.RuleFor(c => c)
            .Must(c =>
            {
                var checkOutTo = ParseTime(c.CheckOutTo);
                var checkInFrom = ParseTime(c.CheckInFrom);
                return checkOutTo <= checkInFrom;
            })
            .When(c => IsValidTime(c.CheckOutTo) && IsValidTime(c.CheckInFrom))
            .WithMessage("Check-out time must be before the next check-in time");

        validator.RuleFor(c => c)
    .Must(c =>
    {
        var checkOutTo = ParseTime(c.CheckOutTo);
        var checkInFrom = ParseTime(c.CheckInFrom);
        return checkInFrom - checkOutTo >= TimeSpan.FromHours(1);
    })
    .When(c => IsValidTime(c.CheckOutTo) && IsValidTime(c.CheckInFrom))
    .WithMessage("There must be at least 1 hour between checkout and next check-in time");

        return validator;
    }
    private static bool IsValidTime(string? time) =>
        !string.IsNullOrWhiteSpace(time) &&
        DateTime.TryParseExact(time, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

    private static TimeSpan ParseTime(string? time)
    {
        DateTime.TryParseExact(time, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed);
        return parsed.TimeOfDay;
    }
}
