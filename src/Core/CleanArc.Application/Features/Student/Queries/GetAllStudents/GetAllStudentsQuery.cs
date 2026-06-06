using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.Student.Queries.GetAllStudents;

public record GetAllStudentsQuery(SearchRequest searchRequest) : IRequest<OperationResult<List<GetAllStudentsQueryResult>>>;
