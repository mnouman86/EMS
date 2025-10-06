using CleanArc.Application.Models.OneBillPayment;
using CleanArc.Application.Profiles;
using CleanArc.Domain.Entities.User;

namespace CleanArc.Application.Features.OneBillPayment.Queries.GetOneBillPayment;

public class GetOneBillPaymentQueryResult
{
    public OneBillInquiryResponseDto Inquiry { get; set; }
}