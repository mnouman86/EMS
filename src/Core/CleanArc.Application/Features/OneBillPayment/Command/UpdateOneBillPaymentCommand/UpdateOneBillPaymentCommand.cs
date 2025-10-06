using CleanArc.Application.Models.Common;
using CleanArc.SharedKernel.ValidationBase;
using CleanArc.SharedKernel.ValidationBase.Contracts;
using FluentValidation;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CleanArc.Application.Models.OneBillPayment;
using System.Text.Json.Serialization; using CleanArc.Domain.Common;
using System.Threading.Tasks;

namespace CleanArc.Application.Features.OneBillPayment.Command.UpdateOneBillPaymentCommand;

public record UpdateOneBillPaymentCommand(OneBillPaymentRequestDto Request) : IRequest<OneBillPaymentResponseDto>
{
    [JsonIgnore]
    public int UserId { get; set; }
}
