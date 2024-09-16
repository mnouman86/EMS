using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.PopularItemsVisit.Queries.GetAllPopularItemsVisit;

public record GetAllPopularItemsVisitQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllPopularItemsVisitQueryResult>>>;

