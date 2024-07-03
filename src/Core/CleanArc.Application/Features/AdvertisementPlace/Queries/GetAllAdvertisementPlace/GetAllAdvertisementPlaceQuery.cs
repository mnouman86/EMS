using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.AdvertisementPlace.Queries.GetAllAdvertisementPlace;

public record GetAllAdvertisementPlaceQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllAdvertisementPlaceQueryResult>>>;

