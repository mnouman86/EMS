using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.PostPaymentStatus.Queries.GetAllPostPaymentStatus;

public record GetAllPostPaymentStatusQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllPostPaymentStatusQueryResult>>>;

