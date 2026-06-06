using CleanArc.Application.Models.Common;
using CleanArc.Application.Models.Request;
using Mediator;

namespace CleanArc.Application.Features.Student.Queries.GetStudentById;

public record GetStudentByIdQuery(SearchRequestById searchRequestById) : IRequest<OperationResult<GetStudentByIdQueryResult>>;
