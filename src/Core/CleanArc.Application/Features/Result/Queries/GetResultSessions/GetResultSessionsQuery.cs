using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.Result.Queries.GetResultSessions;

public record GetResultSessionsQuery(SearchRequest searchRequest)
    : IRequest<OperationResult<List<GetResultSessionsQueryResult>>>;
