using CleanArc.Application.Features.AgeType.Commands.UpdateAgeTypeCommand;
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

namespace CleanArc.Application.Features.BusinessProfile.Command.UpdateBusinessProfileCommand;

public record UpdateBusinessProfileCommand(int Id,
    int? BusinessTypeID, string FullLegalName,
    string MobileNumber,
    string PhoneNumber,
    string EmailAddress,
    int? CountryID,
    int? CityID,
    int? StateID,
    string Address1,
    string Address2,
    string Latitude,
    string Longitude,
    int? ServiceID,
    int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateBusinessProfileCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateBusinessProfileCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateBusinessProfileCommand> validator)
    {
        validator.RuleFor(c => c.BusinessTypeID)
     .NotEmpty()
     .NotNull()
     .WithMessage("Please enter a valid BusinessTypeID ");
        validator.RuleFor(c => c.FullLegalName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a FullLegalName");
        validator.RuleFor(c => c.MobileNumber)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid MobileNumber");
        validator.RuleFor(c => c.PhoneNumber)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid PhoneNumber");
        validator.RuleFor(c => c.EmailAddress)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid EmailAddress");
        validator.RuleFor(c => c.CountryID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid CountryID");
        validator.RuleFor(c => c.CityID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid CityID");
        validator.RuleFor(c => c.Address1)
           .NotEmpty()
           .NotNull()
            .WithMessage("Please enter a valid Address1");
        validator.RuleFor(c => c.Address2)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Address2");
        validator.RuleFor(c => c.Longitude)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a valid Longitude");
        validator.RuleFor(c => c.Latitude)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Latitude");
        validator.RuleFor(c => c.ServiceID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid ServiceID");
        return validator;

       
    }
}

