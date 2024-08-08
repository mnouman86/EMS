using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.DisabilityOption.Queries.GetAllDisabilityOption;

public record GetAllDisabilityOptionQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllDisabilityOptionQueryResult>>>;

