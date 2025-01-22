using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using System.Text.Json.Serialization; 
using CleanArc.Domain.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Domain.Entities.KBTiming;

namespace CleanArc.Application.Features.KBAddress.Commands.CreateKBAddressCommand;
public record CreateKBAddressCommand(
    int? GenericTitleID,
    string? Cost,
    string? Access,
    //string? Availablity,
List<Availability> Timings,
    string? WhenToVisitIDs,
    string? AddressLine1,
    string? AddressLine2,
    int? CountryLookUpID,
    int? CityLookUpID,
    int? StatelookUpID,
    string? PostalCode,
    string? PhoneNo,
    string? Longitude,
    string? Latitude,
    int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateKBAddressCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateKBAddressCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateKBAddressCommand> validator)
    {
        //validator.RuleFor(c => c.Title)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a valid Title");
        //validator.RuleFor(c => c.KeyDate)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a KeyDate");
        //validator.RuleFor(c => c.Cost)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a Cost");
        
        //validator.RuleFor(c => c.RelatedUrlLinkLookupID)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a RelatedUrlLinkLookupID");
        //validator.RuleFor(c => c.Access)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a Access");
        //validator.RuleFor(c => c.Availablity)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a Availablity");

        //validator.RuleFor(c => c.WhenToVisitIDs)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a WhenToVisitIDs");

        //validator.RuleFor(c => c.AddressLine1)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a AddressLine1");
        //validator.RuleFor(c => c.AddressLine2)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a AddressLine2");
        //validator.RuleFor(c => c.CountryLookUpID)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a CountryLookUpID");
        //validator.RuleFor(c => c.CityLookUpID)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a CityLookUpID");
        //validator.RuleFor(c => c.StatelookUpID)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a StatelookUpID");
        return validator;
        //validator.RuleFor(c => c.PostalCode)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a PostalCode");
        //return validator;
        //validator.RuleFor(c => c.Longitude)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a Longitude");
        //return validator;
        //validator.RuleFor(c => c.Latitude)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a Latitude");
        //return validator;
    }
}

