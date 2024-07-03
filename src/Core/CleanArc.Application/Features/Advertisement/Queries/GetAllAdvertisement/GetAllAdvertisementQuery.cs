using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.Advertisement.Queries.GetAllAdvertisement;

public record GetAllAdvertisementQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllAdvertisementQueryResult>>>;

