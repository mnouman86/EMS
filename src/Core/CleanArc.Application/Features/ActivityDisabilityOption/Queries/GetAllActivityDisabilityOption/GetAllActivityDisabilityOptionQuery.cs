using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityDisabilityOption.Queries.GetAllActivityDisabilityOption;

public record GetAllActivityDisabilityOptionQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityDisabilityOptionQueryResult>>>;

