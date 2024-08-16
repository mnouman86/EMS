using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityIDImageMapping.Queries.GetAllActivityIDImageMapping;

public record GetAllActivityIDImageMappingQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityIDImageMappingQueryResult>>>;

