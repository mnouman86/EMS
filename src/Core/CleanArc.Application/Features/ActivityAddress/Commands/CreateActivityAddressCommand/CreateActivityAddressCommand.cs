using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.ActivityAddress.Commands.CreateActivityAddressCommand;
public record CreateActivityAddressCommand(int? GenericAddressID, int? ServiceID, int? CountryLookUpID, int? CityLookUpID, string? AddressLine1,
    string? AddressLine2,
    int? StateLookUpID,
    string? PostalCode,
    string? Latitude,
    string? Longitude,
    int? CultureId,
    int? CreatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreateActivityAddressCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreateActivityAddressCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreateActivityAddressCommand> validator)
    {
        validator.RuleFor(c => c.CountryLookUpID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid CountryLookUpID");
        validator.RuleFor(c => c.CityLookUpID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a CityLookUpID");
        //validator.RuleFor(c => c.AddressLine1)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a AddressLine1");
        //validator.RuleFor(c => c.AddressLine2)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a AddressLine2");
        validator.RuleFor(c => c.StateLookUpID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a State");
        validator.RuleFor(c => c.PostalCode)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a PostalCode");
        validator.RuleFor(c => c.Latitude)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Latitude");
        validator.RuleFor(c => c.Longitude)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Longitude");
        return validator;
    }
}

