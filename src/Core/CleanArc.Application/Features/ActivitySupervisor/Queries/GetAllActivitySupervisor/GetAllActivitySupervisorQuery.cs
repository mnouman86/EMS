using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivitySupervisor.Queries.GetAllActivitySupervisor;

public record GetAllActivitySupervisorQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivitySupervisorQueryResult>>>;

