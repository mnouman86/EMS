using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityAddressMapping.Queries.GetAllActivityAddressMapping;

public record GetAllActivityAddressMappingQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityAddressMappingQueryResult>>>;

