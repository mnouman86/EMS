using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.PopularItemsCityWise.Queries.GetAllPopularItemsCityWise;

public record GetAllPopularItemsCityWiseQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllPopularItemsCityWiseQueryResult>>>;

