using CleanArc.Application.Common.Validation;
using CleanArc.Application.Features.AgeType.Commands.CreateAgeTypeCommand;
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

namespace CleanArc.Application.Features.Hotel.Command.CreateHotelCommand;

public record CreateHotelCommand(
    string? Name,
    int? BusinessId,
    int? ThirdPartyStayId,
    int? Stars,
    string? PostalCode,
    string? AddressLine1,
    string? AddressLine2,
    int? CountryLookUpId,
    int? StateLookUpId,
    int? CityLookUpId,
    int[]? LanguageLookUpId,
    string? Latitude,
    string? Longitude,
    string? MobileNumber,
    string? PhoneNumber,
    string? Email,
    string? FocalPersonName,
    bool? IsChanelManager,
    bool? IsRating,
    bool? IsChain,
    int? ServiceId,
    int? ServiceCategoryId,
    string? CheckInFrom,
    string? CheckInTo,
    string? CheckOutFrom,
    string? CheckOutTo,
    string? About,
    int? CultureId,
    string? RefundPolicy,
    string? NonRefundPolicy,
    string? CancellationPolicy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateHotelCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }

    public IValidator<CreateHotelCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateHotelCommand> validator)
    {
        // Required text fields
        validator.RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Hotel name is required");

        validator.RuleFor(c => c.AddressLine1)
            .NotEmpty().WithMessage("Address Line 1 is required");

        validator.RuleFor(c => c.FocalPersonName)
            .NotEmpty().WithMessage("Focal person name is required");

        validator.RuleFor(c => c.Email)
            .ValidEmail();

        // Required lookups
        validator.RuleFor(c => c.CountryLookUpId)
            .NotNull().WithMessage("Country is required");

        validator.RuleFor(c => c.StateLookUpId)
            .NotNull().WithMessage("State is required");

        validator.RuleFor(c => c.CityLookUpId)
            .NotNull().WithMessage("City is required");

        validator.RuleFor(c => c.LanguageLookUpId)
            .NotNull().WithMessage("Languages are required")
            .Must(arr => arr != null && arr.Length > 0)
            .WithMessage("At least one language must be selected");

        validator.RuleFor(c => c.Latitude)
            .NotEmpty().WithMessage("Latitude is required");

        validator.RuleFor(c => c.Longitude)
            .NotEmpty().WithMessage("Longitude is required");

        validator.RuleFor(c => c.ServiceId)
            .NotNull().WithMessage("Service is required");

        validator.RuleFor(c => c.ServiceCategoryId)
            .NotNull().WithMessage("Service category is required");

        // Time format regex: "01:05 PM", "1:05 AM", "12:00 PM"
        const string timeRegex = @"^(0?[1-9]|1[0-2]):([0-5][0-9]) (AM|PM)$";
        const string timeFormat = "h:mm tt";

        validator.RuleFor(c => c.CheckInFrom)
            .NotEmpty().WithMessage("Check-in from time is required")
            .Matches(timeRegex).WithMessage("Check-in from time must be in 12-hour format (e.g., 3:00 PM)");

        validator.RuleFor(c => c.CheckInTo)
            .NotEmpty().WithMessage("Check-in to time is required")
            .Matches(timeRegex).WithMessage("Check-in to time must be in 12-hour format (e.g., 4:00 PM)");

        validator.RuleFor(c => c.CheckOutFrom)
            .NotEmpty().WithMessage("Check-out from time is required")
            .Matches(timeRegex).WithMessage("Check-out from time must be in 12-hour format (e.g., 10:00 AM)");

        validator.RuleFor(c => c.CheckOutTo)
            .NotEmpty().WithMessage("Check-out to time is required")
            .Matches(timeRegex).WithMessage("Check-out to time must be in 12-hour format (e.g., 12:00 PM)");

        // Time range logic
        validator.RuleFor(c => c)
            .Must(c => ParseTime(c.CheckInFrom) < ParseTime(c.CheckInTo))
            .When(c => IsValidTime(c.CheckInFrom) && IsValidTime(c.CheckInTo))
            .WithMessage("Check-in 'From' time must be earlier than 'To' time");

        validator.RuleFor(c => c)
            .Must(c => ParseTime(c.CheckOutFrom) < ParseTime(c.CheckOutTo))
            .When(c => IsValidTime(c.CheckOutFrom) && IsValidTime(c.CheckOutTo))
            .WithMessage("Check-out 'From' time must be earlier than 'To' time");

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
