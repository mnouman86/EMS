using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityImageMapping.Queries.GetAllActivityImageMapping;

public record GetAllActivityImageMappingQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityImageMappingQueryResult>>>;

