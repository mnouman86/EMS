using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using System.Text.Json.Serialization;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.Activity.Commands.CreateActivityCommand;
public record CreateActivityCommand(
   int? CultureId,
   int? BusinessId,
    string? Title,
    bool? Status,
   int[]? LanguageLookUpId,
   int? ServiceCategoryId,
   int? SubServiceCategoryId,
   string? OtherSubService,
   int? MinAge,
   int? MaxAge,
   int? ActivityTypeLookUpId,
   int? ActivityNatureLookUpId,
   int? MaxGroupSize,
   //bool? IsPrivateActivity,
   string? WhoCanParticipate,
   string? WhoCannotParticipate,
   int? ActivitySupervisorLookUpId,
   string? OtherManageActivity,
   int? Days,
   int? Hours,
    string? Description,
    bool? IsTransportation,
    int? TransportationLookUpId,
    bool? IsDisability,
    string ? AllowedItems,
    string?   NotAllowedItems,
    int? CurrencyLookUpId,
     //Decimal? PerPersonPrice,
     int[]? SeasonLookUpId,
    int[]? IncludeOptionLookUpId,
    int[]? DisabilityOptionLookUpId,
    DateTime? EndDate,
    DateTime? StartDate,
    string? EndTime,
    string? StartTime, bool? IsPartiallyRefundable, bool? IsFullyRefundable,
    string? RefundPolicy, string? NonRefundPolicy, string? CancellationPolicy


    ) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateActivityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityCommand> validator)
    {
        validator.RuleFor(c => c.StartTime)
            .NotEmpty()
    .NotNull()
    .Matches(@"^(0?[1-9]|1[0-2]):([0-5]?[0-9]) (AM|PM)$")
    .WithMessage("CheckInFrom must be in a valid 12-hour format (e.g., 1:05 AM, 12:00 PM, 01:5 PM)");

        validator.RuleFor(c => c.EndTime)
            .NotEmpty()
    .NotNull()
    .Matches(@"^(0?[1-9]|1[0-2]):([0-5]?[0-9]) (AM|PM)$")
    .WithMessage("CheckInFrom must be in a valid 12-hour format (e.g., 1:05 AM, 12:00 PM, 01:5 PM)");

        validator.RuleFor(c => c.BusinessId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please select a business");
        validator.RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Title");
        validator.RuleFor(c => c.LanguageLookUpId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a LanguageLookUpID");

        validator.RuleFor(c => c.WhoCanParticipate)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a WhoCanParticipate");

        validator.RuleFor(c => c.WhoCanParticipate)
        .NotEmpty()
        .NotNull()
        .WithMessage("Please enter a WhoCanParticipate");
        validator.RuleFor(c => c.Days)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Days");
        validator.RuleFor(c => c.Hours)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Hours");
        validator.RuleFor(c => c.SeasonLookUpId)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a SeasonLookUpID");
        //validator.RuleFor(c => c.Description)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.AllowedItems)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a AllowedItems");
        validator.RuleFor(c => c.CurrencyLookUpId)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a CurrencyLookUpID");
   
        return validator;
    }
}

