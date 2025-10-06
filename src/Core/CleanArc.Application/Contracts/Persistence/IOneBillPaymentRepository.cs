using CleanArc.Domain.Entities.OneBillPayment;
using CleanArc.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Common;
using CleanArc.Application.Models.OneBillPayment;

namespace CleanArc.Application.Contracts.Persistence;

public  interface IOneBillPaymentRepository
{
    Task<SingleResponseWrapper<OneBillInquiryResponseDto>> GetOneBillPaymentAsync(string utilityConsumerNumber, string utilityCompanyId);
    Task<string> RecordPaymentAsync(OneBillPaymentRequestDto paymentRequest, CancellationToken cancellationToken);

}
