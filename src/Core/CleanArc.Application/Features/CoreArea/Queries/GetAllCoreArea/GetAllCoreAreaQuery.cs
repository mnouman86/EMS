using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.CoreArea.Queries.GetAllCoreArea;

public record GetAllCoreAreaQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllCoreAreaQueryResult>>>;

