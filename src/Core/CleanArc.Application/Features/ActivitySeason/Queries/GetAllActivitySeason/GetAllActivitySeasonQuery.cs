using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivitySeason.Queries.GetAllActivitySeason;

public record GetAllActivitySeasonQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivitySeasonQueryResult>>>;

