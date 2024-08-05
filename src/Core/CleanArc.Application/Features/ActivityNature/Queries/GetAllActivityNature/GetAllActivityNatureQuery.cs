using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityNature.Queries.GetAllActivityNature;

public record GetAllActivityNatureQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityNatureQueryResult>>>;

