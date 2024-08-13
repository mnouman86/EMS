using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityIncludeOptionMapping.Queries.GetAllActivityIncludeOptionMapping;

public record GetAllActivityIncludeOptionMappingQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityIncludeOptionMappingQueryResult>>>;

