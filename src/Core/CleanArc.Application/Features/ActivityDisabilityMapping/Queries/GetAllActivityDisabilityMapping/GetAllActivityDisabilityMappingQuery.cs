using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityDisabilityMapping.Queries.GetAllActivityDisabilityMapping;

public record GetAllActivityDisabilityMappingQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityDisabilityMappingQueryResult>>>;

