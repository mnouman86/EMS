using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.AgeType.Queries.GetAllAgeType;

public record GetAllAgeTypeQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllAgeTypeQueryResult>>>;

