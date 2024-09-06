using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBCRelatedUrlLink.Queries.GetAllKBCRelatedUrlLink;

public record GetAllKBCRelatedUrlLinkQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllKBCRelatedUrlLinkQueryResult>>>;

