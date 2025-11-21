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
public record UpdatePostPaymentStatusCommand(int Id, string? Bill_Status,
    string? Initiator,
    string? Instrument_Institution,
    string? Instrument_Number,
    string? Instrument_Type,
    string? Message,
    decimal Paid_Amount,
    string? Payment_Channel,
    string? Payment_Link,
    string? Reference_Number,
    int Status,
    string? Transaction_Date_Time,
    string? Transaction_Ref_Id,
    string? OrderNumber,
    int? CultureId,
     int? UpdatedBy) : IRequest<OperationResult<ResponseEntity>>,
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

