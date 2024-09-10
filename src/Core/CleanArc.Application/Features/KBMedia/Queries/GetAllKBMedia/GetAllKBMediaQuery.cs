using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBMedia.Queries.GetAllKBMedia;

public record GetAllKBMediaQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllKBMediaQueryResult>>>;

