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
using CleanArc.Application.Models.Request;using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.ActivityAddress.Commands.UpdateGenericAddressCommand;
public record UpdateGenericAddressCommand(int Id, int? GenericTitleId, int? CountryLookUpId, int? CityLookUpId, string? AddressLine1,
    string? AddressLine2,
    int? StateLookUpId,
    string? PostalCode,
    string? Latitude,
    string? Longitude,
    int? CultureId) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateGenericAddressCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateGenericAddressCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateGenericAddressCommand> validator)
    {
        validator.RuleFor(c => c.CountryLookUpId)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid CountryLookUpID");
        validator.RuleFor(c => c.CityLookUpId)
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
        validator.RuleFor(c => c.StateLookUpId)
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

