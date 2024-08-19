using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ProcessOrder.Queries.GetAllProcessOrder;

public record GetAllProcessOrderQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllProcessOrderQueryResult>>>;

