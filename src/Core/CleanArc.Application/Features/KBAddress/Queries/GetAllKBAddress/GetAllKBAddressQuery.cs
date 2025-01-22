using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBAddress.Queries.GetAllKBAddress;

public record GetAllKBAddressQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllKBAddressQueryResult>>>;

