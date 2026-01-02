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

namespace CleanArc.Application.Features.PostPaymentStatus.Commands.UpdatePostPaymentStatusCommand;
public record UpdatePostPaymentStatusCommand(int Id, string? BillStatus,
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
    string? OrderNumber,
    int? CultureId,
     int? UpdatedBy,
     string PSID,
     string ApplicableSoc,
     string BillerActualSettlementDateTime,
     string BillerSettlementAmount,
     string BillerSettlementDate,
     string BillerSettlementRefId,
     string BillerSettlementStatus,
     string BusinessCrn,
     decimal Fee,
     string FeeChargingType,
     string Qr,
     string SettlementInstitution,
     decimal TaxOnFee) : IRequest<OperationResult<ResponseEntity>>,
    IValidatableModel<UpdatePostPaymentStatusCommand>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public IValidator<UpdatePostPaymentStatusCommand> ValidateApplicationModel(ApplicationBaseValidationModelProvider<UpdatePostPaymentStatusCommand> validator)
    {
        validator.RuleFor(c => c.Id)
     .NotEmpty()
     .NotNull()
     .WithMessage("Please enter a valid Id");
        


        return validator;
    }
}

