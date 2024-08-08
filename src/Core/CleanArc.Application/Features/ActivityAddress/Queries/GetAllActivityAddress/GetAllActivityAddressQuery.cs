using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityAddress.Queries.GetAllActivityAddress;

public record GetAllActivityAddressQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityAddressQueryResult>>>;

