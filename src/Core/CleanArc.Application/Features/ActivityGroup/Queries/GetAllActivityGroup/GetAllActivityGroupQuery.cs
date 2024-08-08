using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityGroup.Queries.GetAllActivityGroup;

public record GetAllActivityGroupQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityGroupQueryResult>>>;

