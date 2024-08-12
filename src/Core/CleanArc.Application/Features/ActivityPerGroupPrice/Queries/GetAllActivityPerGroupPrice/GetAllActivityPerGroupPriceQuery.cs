using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityPerGroupPrice.Queries.GetAllActivityPerGroupPrice;

public record GetAllActivityPerGroupPriceQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityPerGroupPriceQueryResult>>>;

