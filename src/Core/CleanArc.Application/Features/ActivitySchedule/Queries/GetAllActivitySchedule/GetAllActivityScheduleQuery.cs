using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.ActivitySchedule.Queries.GetAllActivitySchedule;

public record GetAllActivityScheduleQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllActivityScheduleQueryResult>>>;

