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

namespace CleanArc.Application.Features.ActivityAddress.Commands.UpdateActivityAddressCommand;
public record UpdateActivityAddressCommand(int ID, string? Country, string? City, string? AddressLine1,
    string? AddressLine2,
    string? State,
    string? PostalCode,
    string? Latitude,
    string? Longitude,
     int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateActivityAddressCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateActivityAddressCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateActivityAddressCommand> validator)
    {
        validator.RuleFor(c => c.Country)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Country");
        validator.RuleFor(c => c.State)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a State");
        validator.RuleFor(c => c.City)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a City");
        validator.RuleFor(c => c.AddressLine1)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a AddressLine1");
        validator.RuleFor(c => c.AddressLine2)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a AddressLine2");
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

