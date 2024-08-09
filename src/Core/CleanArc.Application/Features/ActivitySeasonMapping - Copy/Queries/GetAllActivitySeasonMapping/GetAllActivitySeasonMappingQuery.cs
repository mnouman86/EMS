using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivitySeasonMapping.Queries.GetAllActivitySeasonMapping;

public record GetAllActivitySeasonMappingQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivitySeasonMappingQueryResult>>>;

