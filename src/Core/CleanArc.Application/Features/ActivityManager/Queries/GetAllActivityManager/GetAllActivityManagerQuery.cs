using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityManager.Queries.GetAllActivityManager;

public record GetAllActivityManagerQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityManagerQueryResult>>>;

