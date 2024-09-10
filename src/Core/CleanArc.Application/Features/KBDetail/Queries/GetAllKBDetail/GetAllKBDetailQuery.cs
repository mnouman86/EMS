using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBDetail.Queries.GetAllKBDetail;

public record GetAllKBDetailQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllKBDetailQueryResult>>>;

