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

namespace CleanArc.Application.Features.Activity.Commands.UpdateActivityCommand;
public record UpdateActivityCommand(
   int ID,
    int? CultureId,
    string? Title,
   int? LanguageLookUpID,
   int? ServiceLookUpID,
   int? SubServiceLookUpID,
   string? OtherSubService,
   int? MinAge,
   int? MaxAge,
   int? ActivityTypeLookUpID,
   int? ActivityNatureLookUpID,
   int? MaxGroupSize,
   bool? IsPrivateActivity,
   string? WhoCanParticipate,
   string? WhoCannotParticipate,
   int? ManageActivityLookUpID,
   string? OtherManageActivity,
   int? Days,
   int? Hours,
    string? Description,
    bool? IsTransportation,
    int? TransportationLookUpID,
    bool? IsDisability,
    string? AllowedItems,
    string? NotAllowedItems,
    int? CurrencyLookUpID,
   // Decimal? PerPersonPrice,
     string? SeasonLookUpID,
    string? IncludeOptionLookUpID,
    string? DisabilityOptionLookUpID,
     int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateActivityCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityCommand> validator)
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
        validator.RuleFor(c => c.MinAge)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a MinAge");
        validator.RuleFor(c => c.MaxAge)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a MaxAge");
        validator.RuleFor(c => c.ActivityTypeLookUpID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ActivityTypeLookUpID");
        validator.RuleFor(c => c.ActivityNatureLookUpID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ActivityNatureLookUpID");
        //validator.RuleFor(c => c.MinGroupSize)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a MinGroupSize");
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
        //validator.RuleFor(c => c.OtherManageActivity)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a OtherManageActivity");
        validator.RuleFor(c => c.Description)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.IsTransportation)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.TransportationLookUpID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a TransportationLookUpID ");
        //validator.RuleFor(c => c.OtherSubService)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a OtherSubService");
        validator.RuleFor(c => c.IsDisability)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a IsDisability");
        //validator.RuleFor(c => c.DisabilitiesID)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a Description");
        //validator.RuleFor(c => c.Recommendation)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a Recommendation");
        validator.RuleFor(c => c.AllowedItems)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a AllowedItems");
        validator.RuleFor(c => c.CurrencyLookUpID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a CurrencyLookUpID");
        //validator.RuleFor(c => c.PerPersonPrice)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a PerPersonPrice");
        //validator.RuleFor(c => c.PerGroupPrice)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a PerGroupPrice");
        return validator;
    }
}

