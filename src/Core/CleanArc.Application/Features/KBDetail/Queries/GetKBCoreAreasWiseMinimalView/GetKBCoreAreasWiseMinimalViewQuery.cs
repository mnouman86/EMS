using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBDetail.Queries.GetKBCoreAreasWiseMinimalView;

public record GetKBCoreAreasWiseMinimalViewQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetKBCoreAreasWiseMinimalViewQueryResult>>>;

