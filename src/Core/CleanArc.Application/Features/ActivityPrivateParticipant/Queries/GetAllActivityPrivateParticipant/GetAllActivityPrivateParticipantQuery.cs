using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivityPrivateParticipant.Queries.GetAllActivityPrivateParticipant;

public record GetAllActivityPrivateParticipantQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityPrivateParticipantQueryResult>>>;

