using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityTransportation.Queries.GetAllActivityTransportation;

public record GetAllActivityTransportationQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityTransportationQueryResult>>>;

