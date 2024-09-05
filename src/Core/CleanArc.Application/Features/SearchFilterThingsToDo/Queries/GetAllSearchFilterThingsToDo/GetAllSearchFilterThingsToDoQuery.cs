using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;


namespace CleanArc.Application.Features.SearchFilterThingsToDo.Queries.GetAllSearchFilterThingsToDo;

public record GetAllSearchFilterThingsToDoQuery(ThingsToDoSearchFilterRequest thingsToDoSearchFilterRequest) : IRequest<OperationResult<List<GetAllSearchFilterThingsToDoQueryResult>>>;

