using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.Activity.Queries.GetAllActivity;

public record GetAllActivityQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityQueryResult>>>;

