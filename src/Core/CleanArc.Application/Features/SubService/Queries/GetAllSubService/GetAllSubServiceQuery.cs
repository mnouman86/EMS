using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.SubService.Queries.GetAllSubService;

public record GetAllSubServiceQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllSubServiceQueryResult>>>;

