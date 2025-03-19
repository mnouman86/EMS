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

namespace CleanArc.Application.Features.Hotel.Command.UpdateHotelCommand;

public record UpdateHotelCommand(int ID,string? Name, int? CountryID, int? StateID, int? CityID, int? BusinessID ,int? ZipCode, string? Address1,
    string? Address2, string? Latitude, string? Longitude, string? MobileNumber, string? PhoneNumber, string? Email,
    string? FocalPersonName, bool? IsChanelManager, bool? IsRating, bool? IsChain, int? ServiceID, string? CheckInFrom, string? CheckInTo,
    string? CheckOutFrom, string? CheckOutTo, string? About, int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdateHotelCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateHotelCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateHotelCommand> validator)
    {
        validator.RuleFor(c => c.Name)
             .NotEmpty()
             .NotNull()
             .WithMessage("Please enter a valid Name");
        validator.RuleFor(c => c.CountryID)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a CountryID");
        validator.RuleFor(c => c.StateID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a StateID");
        validator.RuleFor(c => c.CityID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a CityID");
        validator.RuleFor(c => c.BusinessID)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please select a business");
        validator.RuleFor(c => c.ZipCode)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a ZipCode");
        validator.RuleFor(c => c.Address1)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Address1");
        validator.RuleFor(c => c.Address2)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Address2");
        //validator.RuleFor(c => c.IsActive)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a IsActive");
        validator.RuleFor(c => c.Latitude)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a Latitude");
        //validator.RuleFor(c => c.IsDeleted)
        //   .NotEmpty()
        //   .NotNull()
        //   .WithMessage("Please enter a valid IsDeleted");
        validator.RuleFor(c => c.Longitude)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid Longitude");
        validator.RuleFor(c => c.MobileNumber)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid MobileNumber");
        validator.RuleFor(c => c.PhoneNumber)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid PhoneNumber");
        validator.RuleFor(c => c.Email)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid Email");
        validator.RuleFor(c => c.FocalPersonName)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid FocalPersonName");
        validator.RuleFor(c => c.IsChanelManager)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid IsChanelManager");
        validator.RuleFor(c => c.IsRating)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a valid IsRating");
        validator.RuleFor(c => c.IsChain)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a IsChain");
        validator.RuleFor(c => c.CheckInFrom)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a CheckInFrom");
        validator.RuleFor(c => c.CheckInTo)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a CheckInTo");
        validator.RuleFor(c => c.CheckOutFrom)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a CheckOutFrom");
        validator.RuleFor(c => c.CheckOutTo)
           .NotEmpty()
           .NotNull()
           .WithMessage("Please enter a CheckOutTo");
        return validator;
    }
}
