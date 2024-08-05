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
   string? Title,
   int? LanguageID,
   int? ServiceID,
   int? BusinessID,
   int? MinAge,
   int? MaxAge,
   int? ActivityTypeID,
   int? ActivityNatureID,
   int? MinGroupSize,
   int? MaxGroupSize,
   int? PrivateParticipantID,
   string? WhoCanParticipate,
   string? WhoCannotParticipate,
   int? ManageActivityID,
   int? Days,
   int? Hours,
   int? AddressID,
   string? Description,
   bool? IsTransportation,
    int? TransportationID,
    int? ActivityIncludeID,
    bool? IsDisability,
    int? DisabilitiesID,
    string? Recommendation,
    string? AllowedItems,
    int? CurrencyID,
    decimal? PerPersonPrice,
    decimal? PerGroupPrice,
    int? SeasonID,
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
        validator.RuleFor(c => c.LanguageID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a LanguageID");
        validator.RuleFor(c => c.ServiceID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ServiceID");
        validator.RuleFor(c => c.BusinessID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a BusinessID");
        validator.RuleFor(c => c.MinAge)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a MinAge");
        validator.RuleFor(c => c.MaxAge)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a MaxAge");
        validator.RuleFor(c => c.ActivityTypeID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ActivityTypeID");
        validator.RuleFor(c => c.ActivityNatureID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ActivityNatureID");
        validator.RuleFor(c => c.MinGroupSize)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a MinGroupSize");
        validator.RuleFor(c => c.MaxGroupSize)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a MaxGroupSize");
        validator.RuleFor(c => c.PrivateParticipantID)
           .NotEmpty()
           .NotNull()
        .WithMessage("Please enter a PrivateParticipantID");

        validator.RuleFor(c => c.WhoCanParticipate)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a WhoCanParticipate");

        validator.RuleFor(c => c.WhoCanParticipate)
        .NotEmpty()
        .NotNull()
        .WithMessage("Please enter a WhoCanParticipate");
        validator.RuleFor(c => c.ManageActivityID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ManageActivityID");
        validator.RuleFor(c => c.Days)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Days");
        validator.RuleFor(c => c.Hours)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Hours");
        validator.RuleFor(c => c.AddressID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a AddressID");
        validator.RuleFor(c => c.Description)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.IsTransportation)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.TransportationID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a TransportationID");
        validator.RuleFor(c => c.ActivityIncludeID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.IsDisability)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a IsDisability");
        validator.RuleFor(c => c.DisabilitiesID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Description");
        validator.RuleFor(c => c.Recommendation)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Recommendation");
        validator.RuleFor(c => c.AllowedItems)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a AllowedItems");
        validator.RuleFor(c => c.CurrencyID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a CurrencyID");
        validator.RuleFor(c => c.PerPersonPrice)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a PerPersonPrice");
        validator.RuleFor(c => c.PerGroupPrice)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a PerGroupPrice");
        validator.RuleFor(c => c.SeasonID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a SeasonID");
        return validator;
    }
}

