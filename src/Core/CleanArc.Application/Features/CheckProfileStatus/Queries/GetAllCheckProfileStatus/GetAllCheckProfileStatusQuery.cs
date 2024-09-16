using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.CheckProfileStatus.Queries.GetAllCheckProfileStatus;

public record GetAllCheckProfileStatusQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllCheckProfileStatusQueryResult>>>;

