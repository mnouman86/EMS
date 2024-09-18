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
using System.Text.Json.Serialization; using CleanArc.Domain.Common;

namespace CleanArc.Application.Features.Business.Command.UpdateBusinessCommand;

public record UpdateBusinessCommand(int ID, int? BusinessTypeID, string? Name,
    string? PhoneNumber,string? MobileNumber, string? Address1,string? Address2,string? Email, string? Latitude,
    string? Longitude, int? CountryID, int? StateID,
    int? CityID, string? TaxIdentificationNumber, string? License,
    string? ProofOfInsurance, int? BankAccountDetailID,
    bool? IsCancelation, bool? IsRefundable, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateBusinessCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateBusinessCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateBusinessCommand> validator)
    {
        validator.RuleFor(c => c.BusinessTypeID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid BusinessTypeID");
        validator.RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a PhoneNumber");
        validator.RuleFor(c => c.MobileNumber)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Mobile Number");
        validator.RuleFor(c => c.Address1)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Present Address");
        validator.RuleFor(c => c.Address2)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Permanent Address");
        validator.RuleFor(c => c.Email)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Email");
        validator.RuleFor(c => c.Latitude)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Latitude");
        validator.RuleFor(c => c.StateID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a StateID");
        validator.RuleFor(c => c.Longitude)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Longitude");
        validator.RuleFor(c => c.CountryID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a CountryID");
        validator.RuleFor(c => c.TaxIdentificationNumber)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a TaxIdentificationNumber");
        validator.RuleFor(c => c.License)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a License");
        validator.RuleFor(c => c.ProofOfInsurance)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ProofOfInsurance");
        //validator.RuleFor(c => c.BankAccountDetailID)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a BankAccountDetailID");
        validator.RuleFor(c => c.IsCancelation)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a IsCancelation");
        return validator;
    }
}

