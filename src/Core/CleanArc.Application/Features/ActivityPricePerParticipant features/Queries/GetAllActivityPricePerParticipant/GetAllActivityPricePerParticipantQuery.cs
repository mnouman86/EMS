using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityPricePerParticipant.Queries.GetAllActivityPricePerParticipant;

public record GetAllActivityPricePerParticipantQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityPricePerParticipantQueryResult>>>;

