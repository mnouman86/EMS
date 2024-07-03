using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.AdvertisementPage.Queries.GetAllAdvertisementPage;

public record GetAllAdvertisementPageQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllAdvertisementPageQueryResult>>>;

