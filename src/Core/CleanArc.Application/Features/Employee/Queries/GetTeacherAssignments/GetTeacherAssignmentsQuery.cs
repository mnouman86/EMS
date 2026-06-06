using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.Employee.Queries.GetTeacherAssignments;

public record GetTeacherAssignmentsQuery(SearchRequestById searchRequestById) : IRequest<OperationResult<List<GetTeacherAssignmentsQueryResult>>>;
