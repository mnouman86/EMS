using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBRelatedUrlLink.Queries.GetAllKBRelatedUrlLink;

public record GetAllKBRelatedUrlLinkQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllKBRelatedUrlLinkQueryResult>>>;

