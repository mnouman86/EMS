using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBWhenToVisit.Queries.GetAllKBWhenToVisit;

public record GetAllKBWhenToVisitQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllKBWhenToVisitQueryResult>>>;

