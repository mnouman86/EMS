using CleanArc.Application.Features.SearchCarImage.Queries.GetByIdSearchCarImage;
using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using CleanArc.Application.Models.OneBillPayment;
using Mediator;
using System.Text.Json.Serialization;

namespace CleanArc.Application.Features.OneBillPayment.Queries.GetOneBillPayment;

public record GetOneBillPaymentQuery(OneBillInquiryRequestDto request) : IRequest<OneBillInquiryResponseDto>;

