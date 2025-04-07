using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityAddress.Queries.GetAllGenericAddress;

public record GetAllGenericAddressQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllGenericAddressQueryResult>>>;

