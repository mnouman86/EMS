using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBTiming.Queries.GetAllKBTiming;

public record GetAllKBTimingQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllKBTimingQueryResult>>>;

