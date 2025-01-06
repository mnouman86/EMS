using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBDetail.Queries.GetKBMinimalView;

public record GetKBMinimalViewQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetKBMinimalViewQueryResult>>>;

