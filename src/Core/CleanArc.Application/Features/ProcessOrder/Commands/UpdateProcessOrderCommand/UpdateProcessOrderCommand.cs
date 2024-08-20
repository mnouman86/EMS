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

namespace CleanArc.Application.Features.ProcessOrder.Commands.UpdateProcessOrderCommand;
public record UpdateProcessOrderCommand(int ID, string? OrderNumber, string? FirstName, string? LastName, string Email,
   string? PhoneNumber,
    string? CardHolderName,
    string? CardName,
    int? CardCVC,
    int? ExpirationMonth,
    int? ExpirationYear,
    int? CountryID,
    int? ZipCode,
    int? CategoryID,
    int? ServiceID,
    int? SubServiceID,
    decimal? Amount,
    DateTime? FromDate,
    DateTime? ToDate,
    int? NoOfAdults,
    int? NoOfChildrens,
    int? NoOfRooms,
    string OrderStatus,
    DateTime? CreditDate,
    int? CultureId,
     int? UpdatedBy) : IRequest<OperationResult<bool>>,
    IValidatableModel<UpdateProcessOrderCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdateProcessOrderCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdateProcessOrderCommand> validator)
    {
        validator.RuleFor(c => c.OrderNumber)
     .NotEmpty()
     .NotNull()
     .WithMessage("Please enter a valid OrderNumber");
        validator.RuleFor(c => c.FirstName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a FirstName");
        validator.RuleFor(c => c.LastName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a LastName");
        validator.RuleFor(c => c.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a Email");
        validator.RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a valid PhoneNumber");
        validator.RuleFor(c => c.CardHolderName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a CardHolderName");
        validator.RuleFor(c => c.CardName)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a CardName");
        validator.RuleFor(c => c.CardCVC)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a CardCVC");
        validator.RuleFor(c => c.ExpirationMonth)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter a ExpirationMonth");
        validator.RuleFor(c => c.ExpirationYear)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a ExpirationYear");
        validator.RuleFor(c => c.CountryID)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a CountryID");
        validator.RuleFor(c => c.ExpirationMonth)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a ExpirationMonth");
        validator.RuleFor(c => c.ZipCode)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a ZipCode");
        validator.RuleFor(c => c.CategoryID)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a CategoryID");
        validator.RuleFor(c => c.ServiceID)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a ServiceID");
        validator.RuleFor(c => c.SubServiceID)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a SubServiceID");
        validator.RuleFor(c => c.Amount)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a Amount");
        validator.RuleFor(c => c.FromDate)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a FromDate");
        validator.RuleFor(c => c.ToDate)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a ToDate");
        validator.RuleFor(c => c.NoOfAdults)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a NoOfAdults");
        validator.RuleFor(c => c.NoOfChildrens)
          .NotEmpty()
          .NotNull()
          .WithMessage("Please enter a NoOfChildrens");
        validator.RuleFor(c => c.NoOfRooms)
        .NotEmpty()
        .NotNull()
        .WithMessage("Please enter a NoOfRooms");
        validator.RuleFor(c => c.OrderStatus)
        .NotEmpty()
        .NotNull()
        .WithMessage("Please enter a OrderStatus");
        validator.RuleFor(c => c.CreditDate)
        .NotEmpty()
        .NotNull()
        .WithMessage("Please enter a CreditDate");


        return validator;
    }
}

