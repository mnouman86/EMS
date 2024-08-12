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

namespace CleanArc.Application.Features.Activity.Commands.CreateActivityCommand;
public record CreateActivityCommand(
    string? Title,
   int? LanguageLookUpID,
   int? ServiceLookUpID,
   int? SubServiceLookUpID,
   //int? BusinessID,
   int? MinAge,
   int? MaxAge,
   int? ActivityTypeLookUpID,
   int? ActivityNatureLookUpID,
   int? MinGroupSize,
   int? MaxGroupSize,
   bool? IsPrivateActivity,
   int? PrivateParticipantLookUpID,
   string? WhoCanParticipate,
   string? WhoCannotParticipate,
   int? ManageActivityLookUpID,
   int? Days,
   int? Hours,
   //int? AddressID,
   string? Description,
   bool? IsTransportation,
    int? TransportationLookUpID,
   // int? ActivityIncludeID,
    bool? IsDisability,
   // int? DisabilitiesID,
   // string? Recommendation,
    string ? AllowedItems,
    string?   NotAllowedItems,
    int? CurrencyLookUpID,
    Decimal? PerPersonPrice,
    string? OtherSubService,
    string? OtherManageActivity,

    // int? PerGroupPrice
    // int? SeasonID
     int? CreatedBy,
    int? CultureId,
    int? Code,
    string? Message
    ) : IRequest<OperationResult<bool>>,
    IValidatableModel<CreateActivityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityCommand> validator)
    {
        validator.RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Title");
        validator.RuleFor(c => c.LanguageLookUpID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a LanguageLookUpID");
        validator.RuleFor(c => c.ServiceLookUpID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ServiceLookUpID");
        validator.RuleFor(c => c.SubServiceLookUpID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a SubServiceLookUpID");
       
       
        validator.RuleFor(c => c.ActivityTypeLookUpID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ActivityTypeLookUpID");
        validator.RuleFor(c => c.ActivityNatureLookUpID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ActivityNatureLookUpID");
        validator.RuleFor(c => c.MinGroupSize)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a MinGroupSize");
        validator.RuleFor(c => c.MaxGroupSize)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a MaxGroupSize");
        validator.RuleFor(c => c.ActivityTypeLookUpID)
           .NotEmpty()
           .NotNull()
        .WithMessage("Please enter a ActivityTypeLookUpID");

        validator.RuleFor(c => c.WhoCanParticipate)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a WhoCanParticipate");

        validator.RuleFor(c => c.WhoCanParticipate)
        .NotEmpty()
        .NotNull()
        .WithMessage("Please enter a WhoCanParticipate");
        validator.RuleFor(c => c.ManageActivityLookUpID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ManageActivityLookUpID");
        validator.RuleFor(c => c.Days)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Days");
        validator.RuleFor(c => c.Hours)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Hours");
        validator.RuleFor(c => c.OtherSubService)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a OtherSubService");
        validator.RuleFor(c => c.Description)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Description");
        //validator.RuleFor(c => c.IsTransportation)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.TransportationLookUpID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a TransportationLookUpID ");
        validator.RuleFor(c => c.OtherManageActivity)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a OtherManageActivity");
   
        validator.RuleFor(c => c.AllowedItems)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a AllowedItems");
        validator.RuleFor(c => c.CurrencyLookUpID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a CurrencyLookUpID");
        validator.RuleFor(c => c.PerPersonPrice)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a PerPersonPrice");
        return validator;
    }
}

