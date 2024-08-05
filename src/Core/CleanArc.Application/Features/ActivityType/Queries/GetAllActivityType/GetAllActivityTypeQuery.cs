using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityType.Queries.GetAllActivityType;

public record GetAllActivityTypeQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityTypeQueryResult>>>;

