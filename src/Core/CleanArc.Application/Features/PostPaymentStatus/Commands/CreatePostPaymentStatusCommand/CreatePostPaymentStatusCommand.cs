using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using CleanArc.SharedKernel.ValidationBase;
using FluentValidation;
using CleanArc.Application.Models.Request;
using System.Text.Json.Serialization; 
using CleanArc.Domain.Common;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Domain;
using CleanArc.Domain.Entities.PostPaymentStatus;

namespace CleanArc.Application.Features.PostPaymentStatus.Commands.CreatePostPaymentStatusCommand;
public record CreatePostPaymentStatusCommand(
    int? CultureId,
   string? BillStatus,
    string? Initiator,
    string? InstrumentInstitution,
    string? InstrumentNumber,
    string? InstrumentType,
    string? Message,
    decimal PaidAmount,
    string? PaymentChannel,
    string? PaymentLink,
    string? ReferenceNumber,
    int Status,
    string? TransactionDateTime,
    string? TransactionRefId,
    string?OrderNumber,
     int? CreatedBy,
     string PSID,
     string ApplicableSoc,
     string BillerActualSettlementDateTime,
     string BillerSettlementAmount,
     string BillerSettlementDate,
     string BillerSettlementRefId, 
     string BillerSettlementStatus,
     string BusinessCrn,
     decimal Fee ,
     string FeeChargingType,
     string Qr ,
     string SettlementInstitution,
     decimal TaxOnFee
    ) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<CreatePostPaymentStatusCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<CreatePostPaymentStatusCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<CreatePostPaymentStatusCommand> validator)
    {
        //validator.RuleFor(c => c.OrderNumber)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a valid OrderNumber");
        validator.RuleFor(c => c.ReferenceNumber)
            .NotEmpty()
            .NotNull()
            .WithMessage("Please enter reference number");
       
        //validator.RuleFor(c => c.CardHolderName)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a CardHolderName");
        //validator.RuleFor(c => c.CardName)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a CardName");
        //validator.RuleFor(c => c.CardCVC)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a CardCVC");
        //validator.RuleFor(c => c.ExpirationMonth)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Please enter a ExpirationMonth");
        //validator.RuleFor(c => c.ExpirationYear)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a ExpirationYear");
        //validator.RuleFor(c => c.CountryID)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a CountryID");
        //validator.RuleFor(c => c.ExpirationMonth)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a ExpirationMonth");
        //validator.RuleFor(c => c.ZipCode)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a ZipCode");
        //validator.RuleFor(c => c.CategoryID)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a CategoryID");
        //validator.RuleFor(c => c.ServiceID)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a ServiceID");
        //validator.RuleFor(c => c.SubServiceID)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a SubServiceID");
        //validator.RuleFor(c => c.Amount)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a Amount");
        //validator.RuleFor(c => c.FromDate)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a FromDate");
        //validator.RuleFor(c => c.ToDate)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a ToDate");
        //validator.RuleFor(c => c.NoOfAdults)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a NoOfAdults");
        //validator.RuleFor(c => c.NoOfChildrens)
        //  .NotEmpty()
        //  .NotNull()
        //  .WithMessage("Please enter a NoOfChildrens");
        //validator.RuleFor(c => c.NoOfRooms)
        //.NotEmpty()
        //.NotNull()
        //.WithMessage("Please enter a NoOfRooms");
        //validator.RuleFor(c => c.OrderStatus)
        //.NotEmpty()
        //.NotNull()
        //.WithMessage("Please enter a OrderStatus");
        //validator.RuleFor(c => c.CreditDate)
        //.NotEmpty()
        //.NotNull()
        //.WithMessage("Please enter a CreditDate");
        
        return validator;
    }
}

