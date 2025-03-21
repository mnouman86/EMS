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
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities.KBTiming;

namespace CleanArc.Application.Features.KBAddress.Commands.UpdateKBAddressCommand;
public record UpdateKBAddressCommand(//int Id,
    int? GenericTitleID,
    string? Cost,
    string? Access,
 string? Availablity,

 List<Availability> Timings,
    string? WhenToVisitIDs,
    string? AddressLine1,
    string? AddressLine2,
    int? CountryLookUpID,
    int? CityLookUpID,
    int? StatelookUpID,
    string? PostalCode,
    string? Longitude,
    string? Latitude, 
    string? PhoneNo, 
    int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateKBAddressCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateKBAddressCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateKBAddressCommand> validator)
    {
     //   validator.RuleFor(c => c.Title)
     //.NotEmpty()
     //.NotNull()
     //.WithMessage("Please enter a valid Title");
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
        return validator;
    }
}

