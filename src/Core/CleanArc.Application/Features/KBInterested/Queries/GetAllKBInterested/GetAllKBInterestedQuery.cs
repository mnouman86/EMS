using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.KBInterested.Queries.GetAllKBInterested;

public record GetAllKBInterestedQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllKBInterestedQueryResult>>>;

