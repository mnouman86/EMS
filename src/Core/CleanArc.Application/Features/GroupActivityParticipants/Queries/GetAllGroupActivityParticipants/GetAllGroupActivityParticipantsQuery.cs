using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.GroupActivityParticipants.Queries.GetAllGroupActivityParticipants;

public record GetAllGroupActivityParticipantsQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllGroupActivityParticipantsQueryResult>>>;

